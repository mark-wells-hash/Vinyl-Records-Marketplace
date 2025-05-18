//import * as React from 'react';

import configData from "../config.json";
import { LockOutlined } from "@mui/icons-material";
import {
    Container,
    CssBaseline,
    Box,
    Avatar,
    Typography,
    TextField,
    Button,
    Grid,
    CircularProgress
} from "@mui/material";
import { useState } from "react";
import { Link } from "react-router-dom";

const Login = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [loading, setLoading] = useState(false);
    // const handleLogin = () => {};
    const handleLogin = (e: React.MouseEvent<HTMLElement>) => {
        e.preventDefault();
        setLoading(true);
        //Authenticate
        console.log(email + " : " + password);
        fetch(configData.SERVER_URL + 'User/Authorize/' + email + '/' + password)
            .then(response => response.json())
            .then(data => {
                //this.setState({ authorized: data });
                console.log("Here data before " + data.userId + ": " + Object.getOwnPropertyNames(data))
                if (data.userId > 0) {
                    console.log("Here data after " + data)
                    data.authdata = window.btoa(email + ':' + password);
                    localStorage.setItem('user', JSON.stringify(data));
                    setLoading(false);
                    window.location.href = window.location.origin;
                }
                else {
                    alert("Either email or password does not match");
                }
            });
    };
    if (loading) {

        return <CircularProgress style={{ color: "gold" }} className="circular" />;
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
                            alignItems: "left",
                            left: "-200px",
                        }}

                    >
                        <Avatar sx={{ m: 1, bgcolor: "#1976d2", left: "165px" }}>
                            <LockOutlined />
                        </Avatar>
                        <div>
                            <Typography variant="h5" sx={{ left: "165px", position: "relative !important" }}>Login</Typography>
                        </div>
                        <Box sx={{ mt: 1 }}>
                            <Grid container spacing={2}>
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
                                        margin="normal"
                                        required
                                        fullWidth
                                        id="email"
                                        label="Email Address"
                                        name="email"
                                        autoFocus
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
                                        margin="normal"
                                        required
                                        fullWidth
                                        id="password"
                                        name="password"
                                        label="Password"
                                        type="password"
                                        value={password}
                                        onChange={(e) => {
                                            setPassword(e.target.value);
                                        }}
                                    />
                                </Grid>
                            </Grid>
                            <Button
                                fullWidth
                                variant="contained"
                                sx={{ mt: 3, mb: 2 }}
                                onClick={handleLogin}
                            >
                                Login
                            </Button>
                            <Grid container justifyContent={"flex-end"}>
                                <Grid item>
                                    <Link to="/register">Don't have an account? Register</Link>
                                </Grid>
                            </Grid>
                        </Box>
                    </Box>
                </Container>
            </div>
        );
    }
};

export default Login
