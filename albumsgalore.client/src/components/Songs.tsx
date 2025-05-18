import { useState, useEffect } from 'react'
import { DataGrid, GridColDef } from '@mui/x-data-grid'
import { CircularProgress} from '@mui/material';
import Box from '@mui/material/Box';
import configData from "../config.json";
import { Link } from 'react-router-dom';
import { getUser } from './CommonFunctions';

const columns: GridColDef[] = [

    {
        field: 'songId', headerName: 'Actions', headerClassName: 'header', width: 90, disableColumnMenu: true,
        sortable: false, filterable: false, renderCell: (params) => (
            <>
                <Link to={`/artist/view/:${params.value}`}><img src="../editInfo.png" alt="Album Details" title="Edit" className="iconSize" /></Link>
                <a href={`/form/${params.value}`}> <img src="../delete.png" alt="Album Delete" title="Delete" className="iconSize" /></a >
            </>
        )
    },
    { field: 'songName', headerName: 'Song', headerClassName: 'header', flex: 1, editable: true },
    { field: 'artistName', headerName: 'Artist', headerClassName: 'header', flex: 1 },
    { field: 'albumName', headerName: 'Record', headerClassName: 'header', flex: 1 },
    { field: 'description', headerName: 'Description', headerClassName: 'header', flex: 1.5, editable: true },
]



const Songs = () => {
    const user = getUser();
    const [tableData, setTableData] = useState([])
    const [loading, setLoading] = useState(false);
    useEffect(() => {
        setLoading(true);
        fetch(configData.SERVER_URL + 'Album/GetSongsByUserId/' + user.userId)
            .then((data) => data.json())
            .then((data) => {
                setTableData(data);
                setLoading(false);
            });

    }, [])
    //<a href="https://www.freepik.com/icons/information/2#uuid=8c3949a8-d38a-4ddd-9ba3-1efab98e984f">Icon by customicondesign_1</a>
    if (loading) {

        return <CircularProgress color="secondary" className="circular" />;
    }
    else {
        return (
            <div className="gridPanel">

                <Box sx={{ '& .MuiDataGrid-root': { border: 0 } }} style={{ borderRadius: 25, top: 100 }}>
                    <DataGrid
                        rows={tableData}
                        columns={columns}
                        //columns={[{ field: 'songId' }, { field: 'songName', editable: true }]} />
                        getRowId={(row) => row.songId}
                        initialState={{
                            pagination: { paginationModel: { pageSize: 10 } },
                        }}
                        getRowHeight={() => "auto"}
                        pageSizeOptions={[5, 10, 25]}
                        autoHeight {...tableData}
                        // {...tableData} disableVirtualization
                        sx={{
                            '& .MuiDataGrid-cell:hover': {
                                color: 'primary.main',
                            },
                            '& .MuiDataGrid-main': {
                                borderRadius: 4,
                                color: 'white'
                            },
                            '& .MuiDataGrid-filler': {
                                backgroundColor: 'black'
                            },
                            "& .MuiDataGrid-sortIcon": {
                                opacity: 'inherit !important',
                                color: "#ffc300",
                            },
                            "& .MuiDataGrid-menuIconButton": {
                                opacity: 'inherit !important',
                                visibility: 'visible',
                                color: "#ffc300",
                                width: "auto"
                            },
                            '& .MuiDataGrid-iconButtonContainer': {
                                visibility: 'visible',
                                width: "auto"
                            },
                        }}
                    />
                </Box>
                <div className="createDiv" >
                    <div className="createLink">
                        <Link type="button" className="createLink" to="/artist/add/0">Create New Song</Link>
                    </div>
                </div>
            </div>
        )
    }
}

export default Songs

//export class ArtistData {
//    artistId: number = 0;
//    name: string = "";
//    genre: string = "";
//    description: string = "";
//    //albumList: Array<any>;
//}  
