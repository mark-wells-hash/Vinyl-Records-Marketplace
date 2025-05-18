import React from "react";
import { RouteComponentProps } from 'react-router';
//import ReactDOM from "react-dom";
import "./Modal.css";
interface ModalDataState {  
    title: string;  
    loading: boolean;
    //albumList: Array<any>;  
    songList: Array<any>; 
    show: string;
    text: string;  
}
export default class Modal extends React.Component<RouteComponentProps<{}>, ModalDataState> {
    constructor(props) {  
		super(props); 
        this.state = { title: "", loading: true, songList: [] };
		console.log("albumid2 " + props.albumid);
        //console.log("onHide " + props.onHide);
        //console.log("test " + props.test);
        //console.log("getModalSongs " + props.onSongs);
        fetch('Artists/GetSongList/' + props.albumid)
            .then(response => response.json())  
            .then(data => {  
                console.log("rendering3 " + data);
                this.setState({ title: "View", loading: false, songList: data, text: "Songs" });  
         });
        this.onClose = this.onClose.bind(this);
}

render() {
    if (!this.props.show) {
      return null;
    }
    
    console.log("rendering2 " +  + this.state.songList);
      
         let contents = this.state.loading  
            ? <p><em>Loading...</em></p>  
            : this.renderCreateForm(this.state.songList);  
       return  <div>  
            
            {contents}  
        </div>;
  }
    onClose = e => {
     this.props.onHide && this.props.onHide(e)
  };
     handleSave = e => {
     this.props.onSongs && this.props.onSongs(e, this.state.songList)
  };
    // this.props.onSongs && this.props.onSongs(e, this.state.songList) &&
renderCreateForm(songList: Array<any>) {
//console.log("song list " + songList[0].songName);    
return (
    
    <div className={"modal"}id={this.state.albumid}>
        <h2 className = "content">Song List</h2>
        <h3 className = "content">{this.props.children}</h3>
    <div className="form-group row" >  
                     <div className="col-md-4"  >  
                       <table className='table' width="500px" >  
                            <thead >  
                                <tr>  
                                    <th></th>    
                                    <th width="40%">Song Name</th>  
                                    <th width="60%">Description</th>     
                                </tr>  
                            </thead>  
                            <tbody>  
                                {songList.map((art, SongId) =>  
                                    <tr key={SongId}>  
                                        <td></td>    
                                        <td width = "40%">{art.songName}</td>  
                                        <td width = "60%">{art.description}</td>  
                                    </tr>  
                                )}  
                            </tbody>  
                        </table>  
                    </div> 
        </div>
<div className="actions">
          <button className="toggle-button" onClick={this.onClose}>
            close
          </button>
        </div> 
    </div>
    )
}
}