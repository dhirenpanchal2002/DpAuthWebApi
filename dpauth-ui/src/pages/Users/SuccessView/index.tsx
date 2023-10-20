import React from 'react'
import { UserDetails } from './../../../DPAuthApi-Client/data-contracts';
import { Card, Typography, Grid, Tooltip, IconButton, Avatar, Box} from '@mui/material';
import ChevronRightOutlinedIcon from '@mui/icons-material/ChevronRightOutlined';
import EmailOutlinedIcon from '@mui/icons-material/EmailOutlined';
import { stringToColor } from '../../../helpers/component-helper';


interface Props
{
  teams: UserDetails[]
}

const SuccessView = ({teams} : Props) => {
  
  return <>{
    teams.map(x => 
        <Card sx={{ p: [1], m : [2]}} key={x.id}>          
          <Grid container>
            <Grid item flexGrow={1}>
              <Box sx={{ display: 'inline-flex', flexGrow:'inherit', flexDirection:['column','row'] }}>
                <Box sx={{ display: 'inline-block', p:'1vh'}}>
                  <Avatar 
                    alt={x.firstName + ' ' + x.lastName}
                    src="/static/images/avatar/1.jpg"
                    sx={{  bgcolor: stringToColor(x.firstName + ' ' + x.lastName), width: 56, height: 56 }}
                    />
                </Box>
                <Box sx={{ display: 'inline-block', p:'1vh'}}>
                  <Typography variant='h6'  sx={{ fontWeight: 'bold' }}>{x.firstName + ' ' + x.lastName}</Typography>                                              
                  <Box padding={'0px'} display={'flex'} justifyItems={'flex-start'} flexDirection={'row'}>
                    <EmailOutlinedIcon fontSize="small"  color="primary" sx={{ pr: '2px'}} />
                    <Typography variant='body2' color="disabled" >
                        {x.emailId}
                    </Typography>              
                  </Box>                 
                </Box>
              </Box>
            </Grid>
            <Grid item sx={{ display:'inline-flex', flexDirection:'row', border:0,  padding:1, alignSelf:'center' }}>
            <Tooltip title="View group detail" enterDelay={500} leaveDelay={200}>
              <IconButton>
                <ChevronRightOutlinedIcon />
              </IconButton>
              </Tooltip>
            </Grid>
          </Grid>  
        </Card>
        
        )
  }</>
}

export default SuccessView;