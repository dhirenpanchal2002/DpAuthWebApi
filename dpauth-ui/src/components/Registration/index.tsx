import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import { AnonymousComponents } from '../../pages/Login/types';
import { useMutation } from 'react-query'
import { RegisterUser } from '../../DPAuthApi-Client/data-contracts';
import { CircularProgress } from '@mui/material';
import { QueryKeys } from '../../DPAuthApi-Client/QueryKeys';
import { useState } from 'react';
import { AxiosError } from 'Axios';
import Typography from '@mui/material/Typography';

interface Props
{
    setComponent: (arg0: AnonymousComponents) => void;
}

const Registration = ({setComponent} : Props) => {

  const [registerUserData,setRegisterUserData] = useState<RegisterUser>({});

  const registerMutate = useMutation(QueryKeys.Registration.fn, {
    onSuccess: (data) => { 
        
        console.log("On Success : Successfully registered user.."); 
        
        const result = data?.data as RegisterUser;

        console.log(result);

        alert("Successfully register new user. Please login with your emailId and password.");

        setComponent(AnonymousComponents.Login);
      },
    onError: (errorAsUnknown) => {
      const error1 = errorAsUnknown as AxiosError;
      console.log("ON Registrion Error Axioserror  " + error1.response?.data);
    }
  });

  
  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    console.log({
      email: data.get('email'),
      lastName: data.get('lastName'),
      firstName: data.get('firstName'),
      password:  data.get('password'),
    });

    if(data.get('firstName')?.valueOf() !== null && data.get('lastName')?.valueOf() !== null
       && data.get('email')?.valueOf() !== null && data.get('password')?.valueOf() !== null)
    {
      setRegisterUserData(
        {
          firstName: data.get('firstName')?.valueOf().toString() , 
          lastName: data.get('lastName')?.valueOf().toString() , 
          emailId: data.get('email')?.valueOf().toString() , 
          userName: data.get('email')?.valueOf().toString() , 
          password : data.get('password')?.valueOf().toString()
        });

      console.log(" state : " + registerUserData);
      
      await registerMutate.mutateAsync(registerUserData)
        .catch((err) => { 
          console.log("Registration Mutate catch : " + err.message); 
        });
    }
  };

  return (
      <>
        <Box
          sx={{
            marginTop: 0,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Sign up
          </Typography>
          <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
              <Grid item xs={12} sm={6}>
                <TextField
                  autoComplete="given-name"
                  name="firstName"
                  required
                  fullWidth
                  id="firstName"
                  label="First Name"
                  autoFocus
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  required
                  fullWidth
                  id="lastName"
                  label="Last Name"
                  name="lastName"
                  autoComplete="family-name"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  id="email"
                  label="Email Address"
                  name="email"
                  autoComplete="email"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  name="password"
                  label="Password"
                  type="password"
                  id="password"
                  autoComplete="new-password"
                />
              </Grid>
              <Grid item xs={12}>
                <FormControlLabel
                  control={<Checkbox value="allowExtraEmails" color="primary" />}
                  label="I want to receive inspiration, marketing promotions and updates via email."
                />
              </Grid>
            </Grid>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Register
            </Button>
            <Grid container justifyContent="flex-end">
              <Grid item>
                <Link href="#" variant="body2" onClick={(e) => {e.preventDefault();
                                                                  setComponent(AnonymousComponents.Login);
                                                              }}>
                  Already have an account? Sign in
                </Link>
              </Grid>
            </Grid>
          </Box>
        </Box>
      </>
  );
}

export default Registration;