import { useState, useEffect } from 'react'
import { DataGrid, GridColDef } from '@mui/x-data-grid'
import { CircularProgress } from '@mui/material';
//, Tooltip
import { Tooltip } from 'react-tooltip';
import Box from '@mui/material/Box';
import configData from "../config.json";
import { Link } from 'react-router-dom';
import { getUser} from './CommonFunctions';

const columns: GridColDef[] = [

    {
        field: 'albumId', headerName: 'Actions', headerClassName: 'header', width: 90, disableColumnMenu: true,
        sortable: false, filterable: false, renderCell: (params) => (
            <>
                <Link to={`/albumAddEdit/${params.value}/${params.row.artistId}/${params.row.artistName}`} ><img src="../editInfo.png" alt="Album Details" title="Edit" className="iconSize" /></Link>
                <a href={`/form/${params.value}`}> <img src="../delete.png" alt="Album Delete" title="Delete" className="iconSize" /></a >
            </>
        )
    },
    { field: 'albumName', headerName: 'Record', headerClassName: 'header', flex: 1 },
    { field: 'artistName', headerName: 'Artist', headerClassName: 'header', flex: 1 },
    { field: 'statusName', headerName: 'Status', headerClassName: 'header', flex: 0.75 },
    {
        field: 'conditionName', headerName: 'Condition', headerClassName: 'header', flex: 1.5, renderCell: (params) => (
            //<Tooltip title={params.row.conditionDescription} >
            //    <span className="table-cell-trucate">{params.value} <img src="../info.png" alt="Album Details" className="iconSizeSmaller" /></span>
            //</Tooltip>
            <>
                {params.value}
                <a data-tooltip-id={params.row.albumName}><img src="../icons8-information-64.png" alt="Album Details" className="iconSizeSmallerNoFloat" /></a>
                <Tooltip id={params.row.albumName} content={params.row.conditionDescription} />
            </>
        )
    },
    { field: 'description', headerName: 'Description', headerClassName: 'header', flex: 2 },
]

const Albums = () => {
    const user = getUser();

    const [tableData, setTableData] = useState([])
    const [loading, setLoading] = useState(true);
    useEffect(() => { 
        fetch(configData.SERVER_URL + 'Album/GetAlbumsByUserId/'+ user.userId)
            .then((data) => data.json())
            .then((data) => {
                setTableData(data);
                console.log("Check how many times this gets called");
                setLoading(false);
            });

    }, [user.userId]) //user, tableData, loading 
    //<a href="https://www.freepik.com/icons/information/2#uuid=8c3949a8-d38a-4ddd-9ba3-1efab98e984f">Icon by customicondesign_1</a>
    if (loading) {

        return <CircularProgress style={{ color: "gold" }} className="circular" />;
    }
    else {
        return (
            <div className="gridPanel">

                <Box sx={{ '& .MuiDataGrid-root': { border: 0 } }} style={{ borderRadius: 10, top: 100 }}>
                    <DataGrid
                        rows={tableData}
                        columns={columns}
                        getRowId={(row) => row.albumId}
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
                                borderRadius: 2.8,
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
                <hr></hr>
                <div className="createDiv" >
                
                    <div className="createLink">
                        <Link type="button" className="createLink" to="/artist/add/0">Create New Album</Link>
                    </div>
                </div>
            </div>
        )
    }
}

export default Albums
