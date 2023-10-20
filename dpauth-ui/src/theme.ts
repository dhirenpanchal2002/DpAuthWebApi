import { createTheme } from '@mui/material/styles';
import { red, green, orange } from '@mui/material/colors';

// A custom theme for this app
const theme = createTheme({
  palette: {
    primary: {
      main: '#0050b3',
    },
    secondary: {
      main: '#096dd9',
    },
    error: {
      main: red.A400,
    },
    success: {
      main: green.A400  
    },
    warning: {
        main: orange.A400
    },
    mode: 'light'
  },
});

export default theme;
