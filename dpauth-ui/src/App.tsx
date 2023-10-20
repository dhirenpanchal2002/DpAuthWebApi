import React, { useEffect, useState } from 'react';
import './App.css';
import Copyright from './components/Copyright/index';
import {Routes, Route, BrowserRouter} from 'react-router-dom';
import Home from './pages/Home';
import Profile from './pages/Profile';
import Groups from './pages/Groups';
import Leaves from './pages/Leaves';
import LoginPage from './pages/Login';
import { QueryClientProvider, QueryClient } from 'react-query';
import AuthContext from './providers/AuthContext';
import {CurrentUser} from './providers/AuthContext/Type';
import TopAppBar from './components/TopAppBar';
import Users from './pages/Users';
import Todos from './pages/Todos';
import { getUser, removeUser, saveUser } from './helpers/login-helper';

function App() {
  const [currentUser, setCurrentUser] = useState<CurrentUser>({
    IsAuthenticated : false,
    UserName: '',
    Email: '',
    FirstName: '',
    LastName: '',
    authToken: ''
  });

  useEffect(() => {
    
    const loggedinUser = getUser();
  
    if(loggedinUser !== null && loggedinUser !== undefined)
      setCurrentUser(loggedinUser);
    
  }, [])
  

  const OnLoginSuccess = (currentUser) => {

      //Set Auth context
      setCurrentUser(currentUser);

       //Save to Local storage
       saveUser(currentUser);
  }

  const OnLogoutSuccess = () => {

    //Set Auth context
    setCurrentUser({    IsAuthenticated :false,
                        UserName: '',
                        Email: '',
                        FirstName: '',
                        LastName: '',
                        authToken: ''});

     //Remove from Local storage
     removeUser();
}

  const queryClient = new QueryClient();

  return (
    <>
        <QueryClientProvider client={queryClient}>
          {currentUser.IsAuthenticated && 
            <AuthContext.Provider value ={currentUser}>
              <BrowserRouter>
                <TopAppBar onLogout={OnLogoutSuccess}> 
                  <Routes>                           
                      <Route path='/' element={<Home />}></Route>          
                      <Route path='/Home' element={<Home />}></Route>
                      <Route path='/Profile' element={<Profile />}></Route>
                      <Route path='/Users' element={<Users />}></Route>
                      <Route path='/Todos' element={<Todos />}></Route>
                      <Route path='/Groups' element={<Groups />}></Route>
                      <Route path='/Leaves' element={<Leaves />}></Route>                     
                  </Routes>                   
                </TopAppBar>  
              </BrowserRouter>
            </AuthContext.Provider>}
          {!currentUser.IsAuthenticated && <LoginPage OnLoginSuccess={OnLoginSuccess} />}
        </QueryClientProvider>
        <Copyright sx={{ mt: 4, mb: 2 }} />
      </>
  );
}

export default App;
