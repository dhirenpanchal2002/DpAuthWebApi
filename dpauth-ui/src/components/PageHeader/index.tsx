import { Box, Typography } from '@mui/material';
import React from 'react'

interface Props
{
    HeaderText: string    
}
const PageHeader = ({HeaderText} : Props) => {
  return (
    <Box sx={{ padding: '2vh', borderBottom: 'solid grey 1px' }}>
      <Typography variant="h5" noWrap component="div" >
          {HeaderText}
      </Typography>
    </Box>
  )
}


export default PageHeader;