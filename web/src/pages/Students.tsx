import React, { useEffect, useState } from 'react';
import { Alert, Button } from '@mui/material';
import RefreshIcon from '@mui/icons-material/Refresh';
import PageContainer from '../components/common/PageContainer';
import DataTable from '../components/common/DataTable';
import type { Column } from '../components/common/DataTable';
import RiskBadge from '../components/common/RiskBadge';
import { api } from '../services/api';
import type { Student } from '../types/student';

const columns: Column<Student>[] = [
  { key: 'name', label: 'Nome', width: '24%' },
  { key: 'course', label: 'Curso', width: '22%' },
  { key: 'semester', label: 'Semestre', align: 'center', width: '9%' },
  {
    key: 'gradeAverage',
    label: 'Média',
    align: 'center',
    width: '9%',
    render: (row) => (row.gradeAverage != null ? row.gradeAverage.toFixed(1) : '-'),
  },
  {
    key: 'attendanceRate',
    label: 'Frequência',
    align: 'center',
    width: '11%',
    render: (row) =>
      row.attendanceRate != null ? `${(row.attendanceRate * 100).toFixed(0)}%` : '-',
  },
  {
    key: 'probability',
    label: 'Probabilidade',
    align: 'center',
    width: '13%',
    render: (row) =>
      row.probability != null ? `${(row.probability * 100).toFixed(1)}%` : '-',
  },
  {
    key: 'riskLevel',
    label: 'Risco',
    align: 'center',
    width: '10%',
    render: (row) => (row.riskLevel ? <RiskBadge risk={row.riskLevel} /> : '-'),
  },
];

const Students: React.FC = () => {
  const [students, setStudents] = useState<Student[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetchStudents = async () => {
    setLoading(true);
    setError(null);
    try {
      const response = await api.get<Student[]>('/students');
      setStudents(response.data);
    } catch {
      setError('Erro ao carregar os alunos. Verifique se a API está em execução.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchStudents();
  }, []);

  return (
    <PageContainer
      title="Alunos"
      subtitle="Lista de alunos e seus respectivos níveis de risco de evasão"
      actions={
        <Button
          variant="outlined"
          startIcon={<RefreshIcon />}
          onClick={fetchStudents}
          disabled={loading}
        >
          Atualizar
        </Button>
      }
    >
      {error && (
        <Alert severity="error" sx={{ mb: 3 }}>
          {error}
        </Alert>
      )}
      <DataTable<Student>
        columns={columns}
        rows={students}
        loading={loading}
        emptyMessage="Nenhum aluno encontrado."
      />
    </PageContainer>
  );
};

export default Students;
