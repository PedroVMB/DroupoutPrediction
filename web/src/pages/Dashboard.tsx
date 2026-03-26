import React from 'react';
import {
  Grid,
  Card,
  CardContent,
  Typography,
  Box,
} from '@mui/material';
import {
  PeopleAlt as PeopleAltIcon,
  TrendingUp as TrendingUpIcon,
  Warning as WarningIcon,
  CheckCircle as CheckCircleIcon,
} from '@mui/icons-material';
import {
  ResponsiveContainer,
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
} from 'recharts';
import PageContainer from '../components/common/PageContainer';

interface StatCardProps {
  title: string;
  value: string | number;
  icon: React.ReactNode;
  color: string;
  description?: string;
}

const StatCard: React.FC<StatCardProps> = ({ title, value, icon, color, description }) => (
  <Card elevation={2} sx={{ height: '100%' }}>
    <CardContent>
      <Box display="flex" justifyContent="space-between" alignItems="flex-start">
        <Box>
          <Typography variant="body2" color="text.secondary" gutterBottom>
            {title}
          </Typography>
          <Typography variant="h4" fontWeight="bold" gutterBottom sx={{ my: 0.5 }}>
            {value}
          </Typography>
          {description && (
            <Typography variant="caption" color="text.secondary">
              {description}
            </Typography>
          )}
        </Box>

        <Box
          sx={{
            backgroundColor: color,
            borderRadius: 2,
            p: 1.2,
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            color: 'white',
            flexShrink: 0,
          }}
        >
          {icon}
        </Box>
      </Box>
    </CardContent>
  </Card>
);

const chartData = [
  { mes: 'Jan', baixo: 40, medio: 24, alto: 10 },
  { mes: 'Fev', baixo: 38, medio: 28, alto: 14 },
  { mes: 'Mar', baixo: 45, medio: 20, alto: 9 },
  { mes: 'Abr', baixo: 42, medio: 26, alto: 12 },
  { mes: 'Mai', baixo: 50, medio: 18, alto: 8 },
  { mes: 'Jun', baixo: 47, medio: 22, alto: 11 },
];

const Dashboard: React.FC = () => {
  return (
    <PageContainer
      title="Dashboard"
      subtitle="Visão geral da previsão de evasão de alunos"
    >
      <Grid container spacing={3}>
        <Grid size={{ xs: 12, sm: 6, md: 3 }}>
          <StatCard
            title="Total de Alunos"
            value="1.248"
            icon={<PeopleAltIcon />}
            color="#1976d2"
            description="Alunos ativos no sistema"
          />
        </Grid>

        <Grid size={{ xs: 12, sm: 6, md: 3 }}>
          <StatCard
            title="Risco Alto"
            value="87"
            icon={<WarningIcon />}
            color="#d32f2f"
            description="Requerem atenção imediata"
          />
        </Grid>

        <Grid size={{ xs: 12, sm: 6, md: 3 }}>
          <StatCard
            title="Risco Médio"
            value="234"
            icon={<TrendingUpIcon />}
            color="#ed6c02"
            description="Monitoramento recomendado"
          />
        </Grid>

        <Grid size={{ xs: 12, sm: 6, md: 3 }}>
          <StatCard
            title="Risco Baixo"
            value="927"
            icon={<CheckCircleIcon />}
            color="#2e7d32"
            description="Situação estável"
          />
        </Grid>

        {/* Chart */}
        <Grid size={{ xs: 12 }}>
          <Card elevation={2}>
            <CardContent>
              <Typography variant="h6" fontWeight="bold" gutterBottom>
                Distribuição de Risco por Mês
              </Typography>
              <Typography variant="body2" color="text.secondary" mb={3}>
                Evolução dos níveis de risco ao longo do semestre
              </Typography>
              <ResponsiveContainer width="100%" height={320}>
                <BarChart data={chartData} margin={{ top: 5, right: 30, left: 0, bottom: 5 }}>
                  <CartesianGrid strokeDasharray="3 3" stroke="#f0f0f0" />
                  <XAxis dataKey="mes" />
                  <YAxis />
                  <Tooltip />
                  <Legend />
                  <Bar dataKey="baixo" name="Baixo" fill="#2e7d32" radius={[4, 4, 0, 0]} />
                  <Bar dataKey="medio" name="Médio" fill="#ed6c02" radius={[4, 4, 0, 0]} />
                  <Bar dataKey="alto" name="Alto" fill="#d32f2f" radius={[4, 4, 0, 0]} />
                </BarChart>
              </ResponsiveContainer>
            </CardContent>
          </Card>
        </Grid>
      </Grid>
    </PageContainer>
  );
};

export default Dashboard;
