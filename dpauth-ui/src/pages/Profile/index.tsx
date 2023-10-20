import { Avatar, Box, Grid, Paper, Typography, styled } from "@mui/material";
import { useContext} from 'react';
import AuthContext from './../../providers/AuthContext';
import ProfilePicture from "../../components/ProfilePicture";

const Profile = () => {

    const authUser = useContext(AuthContext);

    const Item = styled(Paper)(({ theme }) => ({
        backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
        ...theme.typography.body2,
        padding: theme.spacing(1),
        textAlign: 'center',
        color: theme.palette.text.secondary,
      }));
      
    return (
        <div>
            <Grid container spacing={{ xs: 2, sm: 2, md: 4, xl: 8 }} columns={{ xs: 1, sm: 1, md: 2, xl: 2 }}>
                <Grid>
                    <Item>
                        <ProfilePicture FirstName={authUser.FirstName} LastName={authUser.LastName}></ProfilePicture>
                    </Item>
                </Grid>
                <Grid item xs>                  
                    <Item>xs</Item>
                </Grid>
            </Grid>
        </div>
    );

}

export default Profile;