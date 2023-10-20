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
import Typography from '@mui/material/Typography';
import { AnonymousComponents } from '../../pages/Login/types';
import { useMutation } from 'react-query'
import { UserLogin, UserDetails } from '../../DPAuthApi-Client/data-contracts';
import { CircularProgress } from '@mui/material';
import { QueryKeys } from '../../DPAuthApi-Client/QueryKeys';
import { useState } from 'react';
import { AxiosError } from 'Axios';
import {CurrentUser} from '../../providers/AuthContext/Type';
import MessageBar from '../MessageBar';
import { MessageBarProp, MessageType } from '../MessageBar/types';
import isNullorEmpty from '../../helpers/helpers';

interface Props
{
    setComponent: (arg0: AnonymousComponents) => void;
    SetLoginUser : (arg0: CurrentUser) => void;
}
const Login = ({setComponent, SetLoginUser} : Props) => {
  const [userLoginData,setUserLoginData] = useState<UserLogin>();
  const [isError,setIsError] = useState<boolean>(false);
  const [messageProp,setMessageProp] = useState<MessageBarProp>({Text : '', Visible : false});
  

  const loginMutate = useMutation(QueryKeys.AuthLogin.fn, {
    onSuccess: (data) => { 
        
        console.log("On Success : Successfully Logged status "); 
        
        setMessageProp({ Text : "Successfully Logged status", Type : MessageType.Success, Visible : true });
        
        const result = data?.data as UserDetails;

        console.log(result);

        SetLoginUser({
          IsAuthenticated : true,
          UserName: result?.userName != null ? result?.userName : '',
          Email: result?.emailId != null ? result?.emailId : '',
          FirstName: result?.firstName != null ? result?.firstName : '',
          LastName: result?.lastName != null ? result?.lastName : '',
          authToken: result?.authToken != null ? result?.authToken : ''
        });
      },
    onError: (errorAsUnknown) => {
      const error1 = errorAsUnknown as AxiosError;
      console.log("ON Error Axioserror  " + error1.response?.data);
      setMessageProp({ Text : "invalid Username or password. Please try again", Type : MessageType.Error, Visible : true });
    }
  });

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);

    console.log({
      email: data.get('email')?.valueOf(),
      password: data.get('password')?.valueOf(),
      
    });

    if(isNullorEmpty(data.get('email')?.valueOf()) || isNullorEmpty(data.get('password')?.valueOf()))
    {
      setIsError(true);

      console.log('Is error in true : ', isError);
    }
    else
    {
      setIsError(false);

      console.log('Is error in false : ', isError);

      setUserLoginData({userName: data.get('email')?.valueOf().toString() , password : data.get('password')?.valueOf().toString()});

      console.log(" state : " + userLoginData);
      
      await loginMutate.mutateAsync({userName: userLoginData?.userName, password : userLoginData?.password})
       .catch((err) => { 
          console.log(" Mutate catch : " + err.message); 
          setMessageProp({ Text : "invalid Username or password. Please try again", Type : MessageType.Error, Visible : true });
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
          { !loginMutate.isLoading && <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
            </Avatar>
          }          
          { loginMutate.isLoading &&
            <CircularProgress disableShrink />
          }
          <Typography component="h1" variant="h5">
            Sign in
          </Typography>
          <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
            <TextField
              margin="normal"
              required
              fullWidth
              id="email"
              label="Email Address"
              name="email"
              autoComplete="email"
              helperText="Please enter the email"
              autoFocus
              error={isError} 
            />
            <TextField
              margin="normal"
              error={isError} 
              required
              fullWidth
              name="password"
              label="Password"
              type="password"
              id="password"
              helperText="Please enter the password"
              autoComplete="current-password"
            />
            <FormControlLabel
              control={<Checkbox value="remember" color="primary" />}
              label="Remember me"
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Sign In
            </Button>
            <Grid container>
              <Grid item xs>
                <Link href="/ForgotPassword" variant="body2"   onClick={(e) => {
                                                                            e.preventDefault();
                                                                            setComponent(AnonymousComponents.ForgotPassword);
                                                                        }}>
                  Forgot password?
                </Link>
              </Grid>
              <Grid item>
                <Link href="/Register" variant="body2" onClick={(e) => {
                                                                            e.preventDefault();
                                                                            setComponent(AnonymousComponents.Registration);
                                                                        }}>
                  {"Don't have an account? Sign Up"}
                </Link>
              </Grid>
            </Grid>
          </Box>
        </Box>
        <MessageBar messageBarProp={messageProp} />
      </>                                                                
  );
}
export default Login;