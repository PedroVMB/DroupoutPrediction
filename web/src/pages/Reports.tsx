import React, { useState } from 'react';
import {
  Card,
  CardContent,
  CardActions,
  Typography,
  Button,
  Grid,
  CircularProgress,
  Alert,
  Snackbar,
  Divider,
  Box,
} from '@mui/material';
import DownloadIcon from '@mui/icons-material/Download';
import PictureAsPdfIcon from '@mui/icons-material/PictureAsPdf';
import TableChartIcon from '@mui/icons-material/TableChart';
import PageContainer from '../components/common/PageContainer';
import { api } from '../services/api';
import type { Student } from '../types/student';
import * as XLSX from 'xlsx';
import jsPDF from 'jspdf';

interface SnackbarState {
  open: boolean;
  message: string;
  severity: 'success' | 'error';
}

const Reports: React.FC = () => {
  const [loadingExcel, setLoadingExcel] = useState(false);
  const [loadingPdf, setLoadingPdf] = useState(false);
  const [snackbar, setSnackbar] = useState<SnackbarState>({
    open: false,
    message: '',
    severity: 'success',
  });

  const closeSnackbar = () => setSnackbar((prev) => ({ ...prev, open: false }));

  const fetchStudents = async (): Promise<Student[]> => {
    const response = await api.get<Student[]>('/students');
    return response.data;
  };

  const handleExportExcel = async () => {
    setLoadingExcel(true);
    try {
      const students = await fetchStudents();

      const data = students.map((s) => ({
        Nome: s.name,
        Curso: s.course,
        Semestre: s.semester ?? '-',
        Média: s.gradeAverage != null ? s.gradeAverage.toFixed(1) : '-',
        'Frequência (%)': s.attendanceRate != null ? `${(s.attendanceRate * 100).toFixed(0)}%` : '-',
        Risco: s.riskLevel ?? '-',
      }));

      const ws = XLSX.utils.json_to_sheet(data);
      const wb = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'Alunos');
      XLSX.writeFile(wb, 'relatorio-alunos.xlsx');

      setSnackbar({ open: true, message: 'Excel exportado com sucesso!', severity: 'success' });
    } catch {
      setSnackbar({ open: true, message: 'Erro ao exportar Excel.', severity: 'error' });
    } finally {
      setLoadingExcel(false);
    }
  };

  const handleExportPdf = async () => {
    setLoadingPdf(true);
    try {
      const students = await fetchStudents();

      const doc = new jsPDF();
      const pageWidth = doc.internal.pageSize.getWidth();

      // Header
      doc.setFillColor(25, 118, 210);
      doc.rect(0, 0, pageWidth, 30, 'F');
      doc.setTextColor(255, 255, 255);
      doc.setFontSize(16);
      doc.setFont('helvetica', 'bold');
      doc.text('Relatório de Previsão de Evasão', 14, 18);

      doc.setFontSize(9);
      doc.setFont('helvetica', 'normal');
      doc.text(`Gerado em: ${new Date().toLocaleDateString('pt-BR')}`, pageWidth - 14, 18, {
        align: 'right',
      });

      // Table configuration
      const colHeaders = ['Nome', 'Curso', 'Semestre', 'Média', 'Frequência', 'Risco'];
      const colWidths = [55, 45, 22, 20, 20, 22];
      const startX = 14;
      let y = 45;

      // Table header row
      doc.setFillColor(240, 240, 240);
      doc.rect(startX, y - 6, colWidths.reduce((a, b) => a + b, 0), 10, 'F');
      doc.setTextColor(0);
      doc.setFontSize(9);
      doc.setFont('helvetica', 'bold');

      let x = startX;
      colHeaders.forEach((header, i) => {
        doc.text(header, x + 2, y);
        x += colWidths[i];
      });

      y += 8;

      // Table rows
      doc.setFont('helvetica', 'normal');
      students.forEach((student, idx) => {
        if (y > 270) {
          doc.addPage();
          y = 20;
        }

        if (idx % 2 === 0) {
          doc.setFillColor(250, 250, 250);
          doc.rect(startX, y - 5, colWidths.reduce((a, b) => a + b, 0), 9, 'F');
        }

        const row = [
          student.name.length > 22 ? student.name.substring(0, 22) + '…' : student.name,
          student.course.length > 18 ? student.course.substring(0, 18) + '…' : student.course,
          String(student.semester ?? '-'),
          student.gradeAverage != null ? student.gradeAverage.toFixed(1) : '-',
          student.attendanceRate != null ? `${(student.attendanceRate * 100).toFixed(0)}%` : '-',
          student.riskLevel ?? '-',
        ];

        doc.setTextColor(60, 60, 60);
        x = startX;
        row.forEach((cell, i) => {
          doc.text(String(cell), x + 2, y);
          x += colWidths[i];
        });

        y += 9;
      });

      doc.save('relatorio-alunos.pdf');
      setSnackbar({ open: true, message: 'PDF exportado com sucesso!', severity: 'success' });
    } catch {
      setSnackbar({ open: true, message: 'Erro ao exportar PDF.', severity: 'error' });
    } finally {
      setLoadingPdf(false);
    }
  };

  return (
    <PageContainer
      title="Relatórios"
      subtitle="Exporte os dados de previsão de evasão em diferentes formatos"
    >
      <Grid container spacing={3}>
        {/* Excel Card */}
        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
          <Card elevation={2} sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}>
            <CardContent sx={{ flexGrow: 1 }}>
              <Box display="flex" alignItems="center" gap={1.5} mb={1.5}>
                <TableChartIcon color="success" fontSize="large" />
                <Typography variant="h6" fontWeight="bold">
                  Exportar Excel
                </Typography>
              </Box>
              <Divider sx={{ mb: 2 }} />
              <Typography variant="body2" color="text.secondary">
                Exporte a lista completa de alunos com todos os dados e níveis de risco em
                formato Excel (.xlsx), compatível com Microsoft Excel e Google Sheets.
              </Typography>
            </CardContent>
            <CardActions sx={{ px: 2, pb: 2 }}>
              <Button
                variant="contained"
                color="success"
                fullWidth
                startIcon={
                  loadingExcel ? (
                    <CircularProgress size={18} color="inherit" />
                  ) : (
                    <DownloadIcon />
                  )
                }
                onClick={handleExportExcel}
                disabled={loadingExcel}
              >
                {loadingExcel ? 'Exportando...' : 'Exportar Excel'}
              </Button>
            </CardActions>
          </Card>
        </Grid>

        {/* PDF Card */}
        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
          <Card elevation={2} sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}>
            <CardContent sx={{ flexGrow: 1 }}>
              <Box display="flex" alignItems="center" gap={1.5} mb={1.5}>
                <PictureAsPdfIcon color="error" fontSize="large" />
                <Typography variant="h6" fontWeight="bold">
                  Exportar PDF
                </Typography>
              </Box>
              <Divider sx={{ mb: 2 }} />
              <Typography variant="body2" color="text.secondary">
                Gere um relatório PDF formatado com todos os alunos, incluindo cabeçalho com
                data e tabela estilizada, pronto para impressão ou compartilhamento.
              </Typography>
            </CardContent>
            <CardActions sx={{ px: 2, pb: 2 }}>
              <Button
                variant="contained"
                color="error"
                fullWidth
                startIcon={
                  loadingPdf ? (
                    <CircularProgress size={18} color="inherit" />
                  ) : (
                    <PictureAsPdfIcon />
                  )
                }
                onClick={handleExportPdf}
                disabled={loadingPdf}
              >
                {loadingPdf ? 'Gerando...' : 'Exportar PDF'}
              </Button>
            </CardActions>
          </Card>
        </Grid>
      </Grid>

      <Snackbar
        open={snackbar.open}
        autoHideDuration={4000}
        onClose={closeSnackbar}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
      >
        <Alert onClose={closeSnackbar} severity={snackbar.severity} variant="filled">
          {snackbar.message}
        </Alert>
      </Snackbar>
    </PageContainer>
  );
};

export default Reports;
