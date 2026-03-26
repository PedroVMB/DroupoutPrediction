import React from 'react';
import { AppBar, Toolbar, Typography, IconButton, Box } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import SchoolIcon from '@mui/icons-material/School';

interface NavbarProps {
  onMenuToggle: () => void;
  drawerWidth: number;
  open: boolean;
}

const Navbar: React.FC<NavbarProps> = ({ onMenuToggle, drawerWidth, open }) => {
  return (
    <AppBar
      position="fixed"
      elevation={1}
      sx={{
        zIndex: (theme) => theme.zIndex.drawer + 1,
        transition: (theme) =>
          theme.transitions.create(['width', 'margin'], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen,
          }),
        ...(open && {
          marginLeft: drawerWidth,
          width: `calc(100% - ${drawerWidth}px)`,
          transition: (theme) =>
            theme.transitions.create(['width', 'margin'], {
              easing: theme.transitions.easing.sharp,
              duration: theme.transitions.duration.enteringScreen,
            }),
        }),
      }}
    >
      <Toolbar>
        <IconButton
          color="inherit"
          aria-label="toggle sidebar"
          onClick={onMenuToggle}
          edge="start"
          sx={{ mr: 2 }}
        >
          <MenuIcon />
        </IconButton>

        <Box display="flex" alignItems="center" gap={1}>
          <SchoolIcon fontSize="medium" />
          <Typography variant="h6" noWrap component="div" fontWeight="bold">
            Dropout Prediction
          </Typography>
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default Navbar;
