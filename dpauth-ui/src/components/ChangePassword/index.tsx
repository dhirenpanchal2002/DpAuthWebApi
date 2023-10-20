import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { AnonymousComponents } from '../../pages/Login/types';

interface Props
{
    setComponent: (arg0: AnonymousComponents) => void;
}

const ChangePassword = ({setComponent} : Props) => {
  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    console.log({
      email: data.get('verificationcode'),
      password: data.get('newpassword'),
    });
  };

  return (
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center'
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Change Password
          </Typography>
          <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1, width : '1' }}>
            <TextField
              margin="normal"
              required
              fullWidth
              id="verificationcode"
              label="Verification Code"
              name="verificationcode"
              autoFocus
            />           
            <TextField
              margin="normal"
              required
              fullWidth
              name="newpassword"
              label="New Password"
              type="password"
              id="newpassword"
            />
            <Box  sx={{
                        marginTop: 1,
                        display: 'flex',
                        flexDirection: 'row',
                        alignItems: 'center'
                    }} >
                <Button
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{ mt: 1, mb: 2, mr : 1, ml : 0 }}
                    >
                    Update Password
                </Button>  
                <Button
                    type="button"
                    fullWidth
                    variant="contained"
                    sx={{ mt: 1, mb: 2, mr : 0, ml : 1 }}
                    onClick={(e) => {
                        e.preventDefault();
                        setComponent(AnonymousComponents.Login);
                    }}
                    >
                    Cancel
                </Button>
            </Box>
          </Box>
        </Box>
      </Container>
  );
}
export default ChangePassword;