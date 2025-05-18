//import { useState, useEffect } from 'react';
//import {Tooltip } from '@mui/material';
//import ZoomInIcon from "@mui/icons-material/ZoomIn";
//import IconButton from "@mui/material/IconButton";
import { Link } from 'react-router-dom';
//import { Tooltip } from 'react-tooltip';
//import 'react-tooltip/dist/react-tooltip.css';
import { getUser } from './CommonFunctions';
//import zIndex from '@mui/material/styles/zIndex';

function Home() {
    const user = getUser();
    if (user != null) {
            return (
                <div className="mainPanel">
                    <h1>Hello {user.firstName}!</h1>
                    <h3>UserId: {user.userId}</h3>
                    <p>You're logged in with React & Basic HTTP Authentication!!</p>
                    <p>
                        <Link to="/logout">Logout</Link>
                    </p>
                </div>
            );
        }
        else {
            return (

                 <div className="mainPanel">
                    <div className="col-md-6 col-md-offset-3">
                
                        <p>You are not currently logged in</p>

                        <p>
                            <Link to="/login">Login</Link>
                        </p>
                    </div>
                 </div>
            )
        }
}

export default Home;