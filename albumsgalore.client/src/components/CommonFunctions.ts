//import { useEffect } from 'react'
const userLoggedIn = localStorage.getItem('user');
export function getUser() {
    let user = null;
    if (userLoggedIn) {
        user = JSON.parse(userLoggedIn);
    }
    return user;
}

export function getThumbnail() {
    console.log("getThumbnail");
    //fetch('https://api.discogs.com/database/search?q=Led%20Zepplin&title=Houses%20of%20the%20holy&key=JlfkpVDRbNEVDhqVBpoS&secret=yhatsZVudGZzExRllgDWPzdOrylLHEIR')
    //    .then((data) => data.json())
    //    .then((data) => {
    //        console.log(data);
    //        });
    return "../delete.png";
}
