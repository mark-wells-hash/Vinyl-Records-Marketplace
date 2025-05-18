import { Component } from 'react';
//import { Route } from 'react-router';
import { Routes, Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home';
import Search from './components/Search';
import Artists from './components/Artists';
import Albums  from './components/Albums';
import Songs from './components/Songs';
import Events from './components/Events';
import Offers from './components/Offers';
import Payments from './components/Payments';
import Uploads from './components/Uploads';
import ArtistAddEdit from './components/ArtistAddEdit';
import AlbumDetail from './components/AlbumDetail';
import AlbumEdit from './components/AlbumAddEdit';
import ArtistDetails from './components/ArtistDetails';
//import { EditDetails } from './components/EditDetails';
import Login from './components/Login';
import Logout from './components/Logout';
import ShoppingCart from './components/ShoppingCart'
import Register from './components/Register';
import Profile from './components/Profile'
import './css/site.css'

export default class App extends Component {
    static displayName = App.name;
    
    //const userx = localStorage.getItem('user');
    
    //let user = localStorage.getItem('user');
    //if(user) {
    //    setUser(JSON.parse(user));
    //}
    render() {
        return (
            <Layout>
                <Routes>
                    <Route path="/"  element={<Home />} />
                    <Route path='/search' element={<Search />} />
                    <Route path='/artists' element={<Artists />} />
                    <Route path='/albums' element={<Albums />} />
                    <Route path='/songs' element={<Songs />} />
                    <Route path='/events' element={<Events />} />
                    <Route path='/offers' element={<Offers />} />
                    <Route path='/payments' element={<Payments />} />
                    <Route path='/uploads' element={<Uploads />} />
                    <Route path='/albumDetail/:albumId' element={<AlbumDetail />} />
                    <Route path='/albumAddEdit/:albumId/:artistId/:artistName' element={<AlbumEdit />} />
                    <Route path='/artistAddEdit/:artistId' element={<ArtistAddEdit />} />
                    <Route path='/artistDetails/:artistId' element={<ArtistDetails />} />
                {/*<Route path='/artist/edit/:artid' component={AddArtist} />*/}
                
                {/*<Route path='/artist/editview/:artid' component={EditDetails} />*/}
                    {/*<Route path='/artist/add/:artid' component={EditDetails} />*/}
                    <Route path='/shoppingCart' element={<ShoppingCart />} />
                    <Route path='/login' element={<Login />} />
                    <Route path='/logout' element={<Logout />} />
                    <Route path='/register' element={<Register />} />
                    <Route path='/profile' element={<Profile />} />
                </Routes>
            </Layout>
        );
    }
}
