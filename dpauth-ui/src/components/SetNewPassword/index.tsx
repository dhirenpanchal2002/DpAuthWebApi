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
import isNullorEmpty from '../../helpers/helpers';
import { useState } from 'react';
import { MessageBarProp, MessageType } from '../MessageBar/types';
import { UpdateUserPassword } from '../../DPAuthApi-Client/data-contracts';
import { QueryKeys } from '../../DPAuthApi-Client/QueryKeys';
import { AxiosError } from 'Axios';
import { useMutation } from 'react-query';
import { CircularProgress } from '@mui/material';
import MessageBar from '../MessageBar';

interface Props
{
    setComponent: (arg0: AnonymousComponents) => void;
}

const SetNewPassword = ({setComponent} : Props) => {
  const [updateUserPasswordData,setUpdateUserPasswordData] = useState<UpdateUserPassword>();
  const [isError,setIsError] = useState<boolean>(false);
  const [messageProp,setMessageProp] = useState<MessageBarProp>({Text : '', Visible : false});

  const UpdateUserPasswordMutate = useMutation(QueryKeys.UpdateUserPassword.fn, {
    onSuccess: (data) => { 
        console.log("On Success : Successfully updated new Password");         
        setMessageProp({ Text : "Successfully updated new Password", Type : MessageType.Success, Visible : true });
        
        setComponent(AnonymousComponents.Login);
      },
    onError: (errorAsUnknown) => {
      const error1 = errorAsUnknown as AxiosError;
      console.log("ON Error Axioserror  " + error1.response?.data);
      setMessageProp({ Text : "Invalid verification code. Please try again", Type : MessageType.Error, Visible : true });
    }
  });


  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    console.log({
      email: data.get('verificationcode'),
      password: data.get('newpassword'),
    });

    if(isNullorEmpty(data.get('verificationcode')?.valueOf()) || isNullorEmpty(data.get('newpassword')?.valueOf()))
    {
      setIsError(true);
      console.log('Is error in true : ', isError);
    }
    else
    {
      setIsError(false);
      console.log('Is error in false : ', isError);

      setUpdateUserPasswordData({emailId : 'starrterthr@gmail.com', verificationCode: data.get('verificationcode')?.valueOf().toString() , password : data.get('newpassword')?.valueOf().toString()});

      console.log(" state : " + updateUserPasswordData);
      
      await UpdateUserPasswordMutate.mutateAsync(
        { emailId : updateUserPasswordData?.emailId, 
          verificationCode : updateUserPasswordData?.verificationCode, 
          password : updateUserPasswordData?.password
        })
       .catch((err) => { 
          console.log(" Mutate catch : " + err.message); 
          setMessageProp({ Text : "Invalid verification code or password. Please try again", Type : MessageType.Error, Visible : true });
        });
    }
    
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
          { !UpdateUserPasswordMutate.isLoading && <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
            </Avatar>
          }          
          { UpdateUserPasswordMutate.isLoading &&
            <CircularProgress disableShrink />
          }
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
              helperText="Please enter the verification vode"
              autoComplete="verification-code"
              autoFocus
              error={isError} 
            />           
            <TextField
              margin="normal"
              required
              fullWidth
              name="newpassword"
              label="New Password"
              type="password"
              id="newpassword"
              helperText="Please enter the new password"
              autoComplete="new-password"
              error={isError} 
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
        <MessageBar messageBarProp={messageProp} />
      </Container>
  );
}
export default SetNewPassword;

