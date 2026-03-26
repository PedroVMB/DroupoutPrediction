import { useState } from 'react';
import { Routes, Route } from 'react-router-dom';
import { Box, Toolbar, useMediaQuery, useTheme, CssBaseline } from '@mui/material';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import Navbar from './components/layout/Navbar';
import Sidebar, { DRAWER_WIDTH } from './components/layout/Sidebar';
import Dashboard from './pages/Dashboard';
import Students from './pages/Students';
import Reports from './pages/Reports';

const appTheme = createTheme({
  palette: {
    mode: 'light',
    primary: {
      main: '#1976d2',
    },
    background: {
      default: '#f5f7fa',
    },
  },
  shape: {
    borderRadius: 8,
  },
  typography: {
    fontFamily: '"Inter", "Roboto", "Helvetica", "Arial", sans-serif',
  },
  components: {
    MuiCard: {
      styleOverrides: {
        root: {
          borderRadius: 12,
        },
      },
    },
  },
});

// AppShell is responsible for the layout and uses hooks that require ThemeProvider context.
function AppShell() {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('md'));
  const [sidebarOpen, setSidebarOpen] = useState(true);

  const handleMenuToggle = () => setSidebarOpen((prev) => !prev);

  return (
    <Box sx={{ display: 'flex', minHeight: '100vh', bgcolor: 'background.default' }}>
      <Navbar
        onMenuToggle={handleMenuToggle}
        drawerWidth={DRAWER_WIDTH}
        open={sidebarOpen && !isMobile}
      />

      <Sidebar
        open={sidebarOpen}
        onClose={() => setSidebarOpen(false)}
        variant={isMobile ? 'temporary' : 'persistent'}
      />

      <Box
        component="main"
        sx={{
          flexGrow: 1,
          minWidth: 0,
          transition: (t) =>
            t.transitions.create('margin', {
              easing: t.transitions.easing.sharp,
              duration: t.transitions.duration.leavingScreen,
            }),
        }}
      >
        <Toolbar />
        <Routes>
          <Route path="/" element={<Dashboard />} />
          <Route path="/students" element={<Students />} />
          <Route path="/reports" element={<Reports />} />
        </Routes>
      </Box>
    </Box>
  );
}

function App() {
  return (
    <ThemeProvider theme={appTheme}>
      <CssBaseline />
      <AppShell />
    </ThemeProvider>
  );
}

export default App;
