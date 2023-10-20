import { Box, Button, Typography } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import GroupAddIcon from '@mui/icons-material/GroupAdd';
import PeopleAltIcon from '@mui/icons-material/PeopleAlt';

interface Props
{
    headerText: string,
    AddButtonText: string,
    showAddButton : boolean
    onAddClick?: () => void
}

const Header = ({headerText,showAddButton,AddButtonText,onAddClick } : Props) => {
  return (
    <Box sx={{ display:'flex', justifyContent:'space-between', flexDirection:['column','row'], padding: '1vh', margin: '0px'}} bgcolor={(theme) => theme.palette.grey[300]}>        
        <Typography sx={{ pl: '1vh' }}  variant='h5' >
            <PeopleAltIcon />{headerText}
        </Typography>
        <Box  sx={{pr:'1vh'}}>
        {showAddButton && <Button onClick={onAddClick} color="primary" variant="contained" size='medium'>
                            <AddIcon /> {AddButtonText}
                        </Button>
                        }
        </Box>
    </Box>
  )
}
export default Header;