//import * as React from 'react';
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
import { useState } from "react";
import { Link } from "react-router-dom";
import configData from "../config.json";

const Register = () => {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [email, setEmail] = useState("");

    const handleRegister = (e: React.MouseEvent<HTMLElement>) => {
        e.preventDefault();
        const user = {
            "userId": 0,
            "userName": userName,
            "password": password,
            "phoneNumber": phoneNumber,
            "email": email,
            "firstName": firstName,
            "lastName": lastName,
            //"dateRegistered": "2024-08-12T03:39:07.376Z",
            //"lastLogin": "2024-08-12T03:39:07.376Z",
            "isActive": true
        }
        const current = JSON.stringify(user)
        fetch(configData.SERVER_URL + 'User/Create', {
            method: 'POST',
            body: current, //JSON.stringify(this.state.artData) ,
            headers: {
                'Accept': 'application/json',
                "Content-Type": "application/json" 
            },

        }).then((response) => response.json())
            .then((responseJson) => {
                console.log("responseJson " + responseJson);
                window.location.href = window.location.origin;
            })
    }
    //async () => { };

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
                    }}
                >
                    <Avatar sx={{ m: 1, bgcolor: "#1976d2" }}>
                        <LockOutlined />
                    </Avatar>
                    <Typography variant="h5">Register</Typography>
                    <Box sx={{ mt: 3}}>
                        <Grid container spacing={4}>
                            <Grid item xs={12} sx={{
                                '& .MuiInputLabel-root': {
                                    top: -15,
                                    font: 'bold',
                                    color: 'white',
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
                                    top: -15,
                                    font: 'bold',
                                    color: 'white',
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
                                    top: -15,
                                    font: 'bold',
                                    color: 'white',
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
                                    top: -15,
                                    font: 'bold',
                                    color: 'white',
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
                                    top: -15,
                                    font: 'bold',
                                    color: 'white',
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
                                    top: -15,
                                    font: 'bold',
                                    color: 'white',
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
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2, '& .MuiButton-root': { backgroundColor : 'black'} }}
                            onClick={handleRegister}
                        >
                            Register
                        </Button>
                        <Grid container justifyContent="flex-end">
                            <Grid item>
                                <Link to="/login">Already have an account? Login</Link>
                            </Grid>
                        </Grid>
                    </Box>
                </Box>
            </Container>
        </div>
    );
};

export default Register;