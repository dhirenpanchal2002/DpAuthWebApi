import React, { useState } from 'react'
import { Avatar, Box, Button, Card, Typography } from '@mui/material';
import { FileUpload } from '@mui/icons-material';

interface Props
{
    FirstName: string;
    LastName: string;
}
const ProfilePicture = ({FirstName, LastName} : Props) => {
    /*
    const[fullFilepath,setFullFilepath] = useState({});

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);

        setFullFilepath({
            Filepath: data.get('ProfilePicturerFile')
          });

        console.log(fullFilepath);
    }*/

    const [imageUrl, setImageUrl] = useState({});

    const handleFileUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = (event.target.files as FileList)[0];

        if (file === null) {
            alert("Please select a file!");
          }
        else {  
            /* const reader = new FileReader();

            reader.onloadend = () => {
            setImageUrl({ imgUrl: reader.result});
            };
            
            reader.readAsDataURL(file);
            */
            setImageUrl({ imgUrl: file});
            console.log(' imageUrl ', imageUrl);
        }
    };

  return (
    <Card sx={{
        marginTop: 8,
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
      }}>
        <Box component="form" sx={{ mt: 1 }}>
                <Avatar
                    alt="Dhiren Panchal"
                    src="/static/images/avatar/1.jpg"
                    sx={{ width: 256, height: 256}}
                    >
                    <Button className="button" 
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2, display: 'flex' }}
                        component="label"
                        >
                        Upload Photo
                        <FileUpload />
                        <input  id="upload-image"
                                hidden
                                accept="image/*"
                                type="file"
                                onChange={handleFileUpload} />
                    </Button>
                </Avatar>            
            <Typography component="div" variant="h4" align='center'>
                {"Dhiren"} {"Panchal"}
            </Typography>
            <Typography component="div" variant="subtitle2" align='center'>
                {"dhiren.panchal@test.com"}
            </Typography>
        </Box>
    </Card>
  )
}

export default ProfilePicture;
