import React from 'react'
import {CurrentUser} from './Type';

// set the defaults
const AuthContext = React.createContext<CurrentUser>({
  IsAuthenticated : false,
  UserName: '',
  Email: '',
  FirstName: '',
  LastName: '',
  authToken: ''
});

export default AuthContext;