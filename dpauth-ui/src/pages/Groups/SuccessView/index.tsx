import React from 'react'
import { TeamDetails } from './../../../DPAuthApi-Client/data-contracts';
import { Card, Typography, Grid, Tooltip, IconButton, Box, Avatar, Chip} from '@mui/material';
import ChevronRightOutlinedIcon from '@mui/icons-material/ChevronRightOutlined';
import EmailOutlinedIcon from '@mui/icons-material/EmailOutlined';

interface Props
{
  teams: TeamDetails[]
}

const SuccessView = ({teams} : Props) => {

  //const handleTeamLeadClick= (teamleadid: string) => {  }

  return <>{
    teams.map(x => 
        <Card sx={{ p: [2], m : [2]}} key={x.id}>          
          <Grid container>
            <Grid item flexGrow={1}>
              <Box padding={'1px'} display={'flex'} justifyItems={'flex-start'} flexDirection={'row'}  alignSelf={'flex-start'}>                
                <Typography variant='h6' sx={{ fontWeight: 'bold' }}  alignSelf={'flex-start'}>{x.teamName}</Typography>      
              </Box>
              <Box padding={'1px'} display={'flex'} justifyItems={'flex-start'} flexDirection={'row'}>
                <EmailOutlinedIcon fontSize="small"  color="primary" sx={{ pr: '2px'}} />
                <Typography variant='body2' color="disabled">
                    {x.teamEmailId}
                </Typography>              
              </Box>                      
              <Box paddingTop={'3px'} display={'flex'} justifyItems={'flex-start'} flexDirection={'row'}>
                <Chip size='small'
                    avatar={<Avatar alt={x.teamLead?.firstName + ' ' + x.teamLead?.lastName} src={x.teamLead?.photoUrl?.toString()} />}
                    label={x.teamLead?.firstName + ' ' + x.teamLead?.lastName}
                    variant="filled"
                  />            
              </Box> 
            </Grid>
            <Grid item sx={{ display:'inline-flex', flexDirection:'row', border:0,  padding:0, alignSelf:'center'}}>
            <Tooltip title="View group detail" enterDelay={500} leaveDelay={200}>
              <IconButton >
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