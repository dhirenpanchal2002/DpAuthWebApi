// material-ui
import { useMediaQuery, Container, Link, Typography, Stack } from '@mui/material';

// ==============================|| FOOTER - AUTHENTICATION ||============================== //

const Copyright = (props: any) => {
  //const matchDownSM = useMediaQuery((theme) => theme.breakpoints.down('sm'));

  return (
    <Container maxWidth="xl">
      <Stack
        direction={'column'}
        justifyContent={'center'}
        spacing={2}
        textAlign={'center'}
      >
        
          <Typography variant="body2" color="text.secondary" align="center" {...props}>
            {'Copyright © '}
            <Link color="inherit" href="https://mui.com/">
              www.star-hr.com
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
          </Typography>
      </Stack>
    </Container>
  );
};

export default Copyright;
