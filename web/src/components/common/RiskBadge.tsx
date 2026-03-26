import React from 'react';
import { Chip } from '@mui/material';
import type { RiskLevel } from '../../types/student';

interface RiskBadgeProps {
  risk: RiskLevel;
}

const riskConfig: Record<RiskLevel, { label: string; color: 'success' | 'warning' | 'error' }> = {
  LOW: { label: 'Baixo', color: 'success' },
  MEDIUM: { label: 'Médio', color: 'warning' },
  HIGH: { label: 'Alto', color: 'error' },
};

const RiskBadge: React.FC<RiskBadgeProps> = ({ risk }) => {
  const config = riskConfig[risk];

  return (
    <Chip
      label={config.label}
      color={config.color}
      size="small"
      variant="filled"
      sx={{ fontWeight: 600, minWidth: 64 }}
    />
  );
};

export default RiskBadge;
