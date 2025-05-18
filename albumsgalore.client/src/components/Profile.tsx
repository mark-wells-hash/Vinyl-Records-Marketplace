//import * as React from 'react';
import { getUser } from './CommonFunctions';
import {
    Avatar,
    Box,
    Button,
    Container,
    CssBaseline,
    Grid,
    TextField,
    Typography,
} from "@mui/material";
import { LockOutlined } from "@mui/icons-material";
import { useState, useEffect } from "react";
import { CircularProgress } from '@mui/material';
import configData from "../config.json";

const Profile = () => {
    const user = getUser();
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [email, setEmail] = useState("");
    const [edit, setEdit] = useState(false);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetch(configData.SERVER_URL + 'User/GetUser/' + user.userId)
            .then(response => response.json())
            .then(data => {
                //this.setState({ authorized: data });
                //console.log("Here data before " + data.userId + ": " + Object.getOwnPropertyNames(data))
                if (data.userId > 0) {
                    console.log("Here data after " + data)
                    setFirstName(data.firstName);
                    setLastName(data.lastName);
                    setUserName(data.userName);
                    setPassword(data.password);
                    setPhoneNumber(data.phoneNumber);
                    setEmail(data.email);
                    setLoading(false);
                }
                else {
                    alert("Cannot find user");
                }
            });
    }, [])

    const handleEdit = (e: React.MouseEvent<HTMLElement>) => {
        e.preventDefault();
        setEdit(true);
    };
    const handleCancel = (e: React.MouseEvent<HTMLElement>) => {
        e.preventDefault();
        window.location.href = window.location.origin;
    };
    const handleEditCancel = (e: React.MouseEvent<HTMLElement>) => {
        e.preventDefault();
        setEdit(false);
    };
    const handleSave = (e: React.MouseEvent<HTMLElement>) => {
        e.preventDefault();
        const updateUser = {
            "userId": user.userId,
            "userName": userName,
            "password": password,
            "phoneNumber": phoneNumber,
            "email": email,
            "firstName": firstName,
            "lastName": lastName,
            "isActive": true
        }
        const current = JSON.stringify(updateUser)
        console.log(current);
        fetch(configData.SERVER_URL + 'User/UpdateUser', {
            method: 'PUT',
            body: current, //JSON.stringify(this.state.artData) ,
            headers: {
                'Accept': 'application/json',
                "Content-Type": "application/json"
            },

        }).then((response) => response.json())
            .then((responseJson) => {
                console.log("responseJson " + responseJson);
                setLoading(false);
                setEdit(false);
                // window.location.href = window.location.origin;
            })
    }
    //async () => { };
    if (loading) {

        return <CircularProgress style={{ color: "white" }} className="circular" />;
    }
    if (edit) {
        return (
            <div className="container">
                <Container maxWidth="xs">
                    <CssBaseline />
                    <Box
                        sx={{
                            //mt: 20,
                            display: "flex",
                            flexDirection: "column",
                            alignItems: "center",
                            //backgroundColor: '#008aff',
                            borderRadius: 3,
                            color: "white !important"
                        }}
                    >
                        <Avatar sx={{ m: 1, bgcolor: "#1976d2" }}>
                            <LockOutlined />
                        </Avatar>
                        <Typography variant="h5">Edit Profile</Typography>
                        <Box sx={{ mt: 3 }}>
                            <Grid container spacing={4}>
                                <Grid item xs={12} sx={{
                                    '& .MuiInputLabel-root': {
                                        //top: -15,
                                        font: 'bold',
                                        color: 'white',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .Mui-focused': {
                                        //top: -15,
                                        font: 'bold',
                                        color: 'white !important',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .css-14lo706': {
                                        height: '8px'
                                    }
                                }}>
                                    <TextField
                                        name="firstName"
                                        required
                                        fullWidth
                                        id="firstName"
                                        label="First Name"
                                        autoFocus
                                        value={firstName}
                                        onChange={(e) => setFirstName(e.target.value)}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{
                                    '& .MuiInputLabel-root': {
                                        //top: -15,
                                        font: 'bold',
                                        color: 'white',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .Mui-focused': {
                                        //top: -15,
                                        font: 'bold',
                                        color: 'white !important',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .css-14lo706': {
                                        height: '8px'
                                    }
                                }}>
                                    <TextField
                                        name="lastName"
                                        required
                                        fullWidth
                                        id="lastName"
                                        label="Last Name"
                                        autoFocus
                                        value={lastName}
                                        onChange={(e) => setLastName(e.target.value)}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{
                                    '& .MuiInputLabel-root': {
                                        //top: -15,
                                        font: 'bold',
                                        color: 'white',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .Mui-focused': {
                                        //top: -15,
                                        font: 'bold',
                                        color: 'white !important',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .css-14lo706': {
                                        height: '8px'
                                    }
                                }}>
                                    <TextField
                                        name="userName"
                                        required
                                        fullWidth
                                        id="userName"
                                        label="User Name"
                                        autoFocus
                                        value={userName}
                                        onChange={(e) => setUserName(e.target.value)}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{
                                    '& .MuiInputLabel-root': {
                                        //top: -15,
                                        font: 'bold',
                                        color: 'white',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .Mui-focused': {
                                        //top: -15,
                                        font: 'bold',
                                        color: 'white !important',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .css-14lo706': {
                                        height: '8px'
                                    }
                                }}>
                                    <TextField
                                        name="phoneNumber"
                                        required
                                        fullWidth
                                        id="phoneNumber"
                                        label="Phone Number"
                                        autoFocus
                                        value={phoneNumber}
                                        onChange={(e) => setPhoneNumber(e.target.value)}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{
                                    '& .MuiInputLabel-root': {
                                        //top: -15,
                                        font: 'bold',
                                        color: 'white',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .Mui-focused': {
                                        //top: -15,
                                        font: 'bold',
                                        color: 'white !important',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .css-14lo706': {
                                        height: '8px'
                                    }
                                }}>
                                    <TextField
                                        required
                                        fullWidth
                                        id="email"
                                        label="Email Address"
                                        name="email"
                                        value={email}
                                        onChange={(e) => setEmail(e.target.value)}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{
                                    '& .MuiInputLabel-root': {
                                        //top: -15,
                                        font: 'bold',
                                        color: 'white',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .Mui-focused': {
                                        //top: -15,
                                        font: 'bold',
                                        color: 'white !important',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .css-14lo706': {
                                        height: '8px'
                                    }
                                }}>
                                    <TextField
                                        required
                                        fullWidth
                                        name="password"
                                        label="Password"
                                        type="password"
                                        id="password"
                                        value={password}
                                        onChange={(e) => setPassword(e.target.value)}
                                    />
                                </Grid>
                            </Grid>
                            <Button
                               // fullWidth
                                variant="contained"
                                sx={{ mt: 3, mb: 2, width: '50%', '& .MuiButton-root': { backgroundColor: 'black' } }}
                                onClick={handleSave}
                            >
                                Save
                            </Button>
                            <Button
                               // fullWidth
                                variant="contained"
                                sx={{ mt: 3, mb: 2, width:'50%', '& .MuiButton-root': { backgroundColor: 'black' } }}
                                onClick={handleEditCancel}
                            >
                                Cancel
                            </Button>
                        </Box>
                    </Box>
                </Container>
            </div>
        );
    }
    else {
        return (
            <div className="container">
                <Container maxWidth="xs">
                    <CssBaseline />
                    <Box
                        sx={{
                            //mt: 20,
                            display: "flex",
                            flexDirection: "column",
                            alignItems: "center",
                            //backgroundColor: '#008aff',
                            borderRadius: 3,
                            color:"white !important"
                        }}
                    >
                        <Avatar sx={{ m: 1, bgcolor: "#1976d2" }}>
                            <LockOutlined />
                        </Avatar>
                        <Typography variant="h5">Profile</Typography>
                        <Box sx={{ mt: 3 }}>
                            <Grid container spacing={4}>
                                <Grid item xs={12} sx={{
                                    '& .MuiInputLabel-root': {
                                        top: -30,
                                        font: 'bold !important',
                                        color: 'white !important',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .MuiInputBase-input.Mui-disabled': {
                                        WebkitTextFillColor: 'white !important',
                                    }
                                }}>
                                    <TextField
                                        name="firstName"
                                        fullWidth
                                        disabled
                                        variant="filled"
                                        id="firstName"
                                        label="First Name"
                                        autoFocus
                                        value={firstName}
                                        //className="whiteText"
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{
                                    '& .MuiInputLabel-root': {
                                        top: -30,
                                        font: 'bold !important',
                                        color: 'white !important',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .MuiInputBase-input.Mui-disabled': {
                                        WebkitTextFillColor: 'white !important',
                                    }
                                }}>
                                    <TextField
                                        name="lastName"
                                        fullWidth
                                        disabled
                                        variant="filled"
                                        id="lastName"
                                        label="Last Name"
                                        autoFocus
                                        value={lastName}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{
                                    '& .MuiInputLabel-root': {
                                        top: -30,
                                        font: 'bold !important',
                                        color: 'white !important',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .MuiInputBase-input.Mui-disabled': {
                                        WebkitTextFillColor: 'white !important',
                                    }
                                }}>
                                    <TextField
                                        name="userName"
                                        fullWidth
                                        disabled
                                        variant="filled"
                                        id="userName"
                                        label="User Name"
                                        autoFocus
                                        value={userName}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{
                                    '& .MuiInputLabel-root': {
                                        top: -30,
                                        font: 'bold !important',
                                        color: 'white !important',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .MuiInputBase-input.Mui-disabled': {
                                        WebkitTextFillColor: 'white !important',
                                    }
                                }}>
                                    <TextField
                                        name="phoneNumber"
                                        fullWidth
                                        disabled
                                        variant="filled"
                                        id="phoneNumber"
                                        label="Phone Number"
                                        autoFocus
                                        value={phoneNumber}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{
                                    '& .MuiInputLabel-root': {
                                        top: -30,
                                        font: 'bold !important',
                                        color: 'white !important',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .MuiInputBase-input.Mui-disabled': {
                                        WebkitTextFillColor: 'white !important',
                                    }
                                }}>
                                    <TextField
                                        fullWidth
                                        disabled
                                        variant="filled"
                                        id="email"
                                        label="Email Address"
                                        name="email"
                                        value={email}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{
                                    '& .MuiInputLabel-root': {
                                        top: -30,
                                        font: 'bold !important',
                                        color: 'white !important',
                                        fontSize: 'large',
                                        fontWeight: 400
                                    },
                                    '& .MuiInputBase-input.Mui-disabled': {
                                        WebkitTextFillColor: 'white !important',
                                    }
                                }}>
                                    <TextField
                                        fullWidth
                                        disabled
                                        variant="filled"
                                        name="password"
                                        label="Password"
                                        type="password"
                                        id="password"
                                        value={password}
                                    />
                                </Grid>
                            </Grid>
                            <Button
                                fullWidth
                                variant="contained"
                                sx={{ mt: 3, mb: 2, width: '50%', '& .MuiButton-root': { backgroundColor: 'black' } }}
                                onClick={handleEdit}
                            >
                                Edit
                            </Button>
                            <Button
                                // fullWidth
                                variant="contained"
                                sx={{ mt: 3, mb: 2, width: '50%', '& .MuiButton-root': { backgroundColor: 'black' } }}
                                onClick={handleCancel}
                            >
                                Cancel
                            </Button>
                        </Box>
                    </Box>
                </Container>
            </div>
        );
    }
};

export default Profile;