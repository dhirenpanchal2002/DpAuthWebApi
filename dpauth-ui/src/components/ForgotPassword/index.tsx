import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import { AnonymousComponents } from '../../pages/Login/types';
import { MessageBarProp, MessageType } from '../MessageBar/types';
import { QueryKeys } from '../../DPAuthApi-Client/QueryKeys';
import { useMutation } from 'react-query';
import { AxiosError } from 'Axios';
import isNullorEmpty from '../../helpers/helpers';
import { CircularProgress } from '@mui/material';
import MessageBar from '../MessageBar';

interface Props
{
    setComponent: (arg0: AnonymousComponents) => void;
}

const ForgotPassword = ({setComponent} : Props) => {
  //const [updateUserPasswordData,setUpdateUserPasswordData] = useState<UpdateUserPassword>();
  const [isError,setIsError] = React.useState<boolean>(false);
  const [messageProp,setMessageProp] = React.useState<MessageBarProp>({Text : '', Visible : false});

  const sendVerificationCodeMutate = useMutation(QueryKeys.SendVerificationCode.fn, {
    onSuccess: (data) => { 
        console.log("On Success : Successfully sent verification code");         
        setMessageProp({ Text : "Successfully sent verification code", Type : MessageType.Success, Visible : true });
        
        setComponent(AnonymousComponents.SetNewPassword);
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
      email: data.get('email')
    });

    if(isNullorEmpty(data.get('email')?.valueOf()))
    {
      setIsError(true);
      console.log('Is error in true : ', isError);
    }
    else
    {
      setIsError(false);
      console.log('Is error in false : ', isError);

      console.log(" valid email for sending verification code : " + data.get('email'));
      
      await sendVerificationCodeMutate.mutateAsync(
        { emailId : data.get('email')?.toString()
        })
       .catch((err) => { 
          console.log(" Mutate catch : " + err.message); 
          setMessageProp({ Text : "Error while sending verification code. Please try again", Type : MessageType.Error, Visible : true });
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
            alignItems: 'center'
          }}
        >
          { !sendVerificationCodeMutate.isLoading && <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
            </Avatar>
          }          
          { sendVerificationCodeMutate.isLoading &&
            <CircularProgress disableShrink />
          }
          <Typography component="h1" variant="h5">
            Forgot Password
          </Typography>
          <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1, width : '1' }}>
            <TextField
              margin="normal"
              required
              fullWidth
              id="email"
              label="Email Address"
              name="email"
              autoComplete="email"
              autoFocus
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
                    Send Code
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
      </>
  );
}
export default ForgotPassword;