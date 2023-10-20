import * as React from 'react';
import { useState } from 'react';
import Login from '../../components/Login';
import {AnonymousComponents} from './types'
import ForgotPassword from '../../components/ForgotPassword';
import ChangePassword from '../../components/ChangePassword';
import {CurrentUser} from '../../providers/AuthContext/Type';
import Registration from '../../components/Registration';
import { Box, Card, Container, Typography } from '@mui/material';
import StarHalfIcon from '@mui/icons-material/StarHalf';

interface Props
{
  OnLoginSuccess:  (arg0: CurrentUser) => void;
}

const LoginPage = ({OnLoginSuccess} : Props) => {
  const [currentComponent, SetCurrentComponent] = useState(AnonymousComponents.Login);

  console.log("Login page currentComp : ", currentComponent);

  return (
    <>
    <Box  sx={{  backgroundColor: 'primary.dark', display: 'flex', justifyContent:'flex-start',
        minHeight:'30px', boxShadow:'6'}}>
      <StarHalfIcon fontSize='large' sx={{ fontSize: 40, color:'white', mt: '3vh',  ml: '3vh'}} />     
      <Typography  variant='h4' color="white" sx={{padding: '3vh'}} >
          Star HR
      </Typography>
    </Box>
    <Container maxWidth="sm" >      
      <Card sx={{backgroundColor:'white', padding:[4], margin:[4], border:'1px solid lightgrey',
           boxShadow:'6', borderRadius:[4,8]}}>        
        {currentComponent === AnonymousComponents.Login && <Login setComponent={SetCurrentComponent} SetLoginUser={OnLoginSuccess} />}
        {currentComponent === AnonymousComponents.ForgotPassword && <ForgotPassword setComponent={SetCurrentComponent}/>}
        {currentComponent === AnonymousComponents.ChangePassword && <ChangePassword setComponent={SetCurrentComponent}/>}
        {currentComponent === AnonymousComponents.Registration && <Registration setComponent={SetCurrentComponent}/>}
      </Card>
    </Container>
    </>
  );
}
export default LoginPage;