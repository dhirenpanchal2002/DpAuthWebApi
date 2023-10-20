import React from 'react'
import { Box, CircularProgress, Typography } from '@mui/material';

const LoadingView = () => {
  return (
    <Box sx={{display:'flex', justifyContent:'space-around', flexDirection:'column', padding:'5vh'}}>
    <Box sx={{display:'block', flexGrow:'1', alignSelf:'center'}}>
    <CircularProgress />    
    </Box>
    <Box sx={{display:'block', flexGrow:'1', alignSelf:'center', maxHeight:'100%'}}>
      <Typography variant='h6'>
        Loading... Please wait.
      </Typography>
    </Box>      
  </Box>
  )
}


export default LoadingView;