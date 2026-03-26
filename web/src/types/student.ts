export type RiskLevel = 'LOW' | 'MEDIUM' | 'HIGH';

export interface Student {
  id: string;
  name: string;
  course: string;
  semester: number;
  enrollmentDate: string;
  attendanceRate: number;     // 0–1 (e.g. 0.92 = 92% attendance)
  gradeAverage: number;       // 0–10 scale
  assignmentsCompleted: number;
  lastAccessDaysAgo: number;
  riskLevel: RiskLevel;
  probability: number;        // 0–1
}

export interface Prediction {
  studentId: string;
  probability: number;
  riskLevel: RiskLevel;
  generatedAt: string;
}

export interface StudentWithPrediction extends Student {
  prediction?: Prediction;
}