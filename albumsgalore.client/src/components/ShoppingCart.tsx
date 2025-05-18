import { useState, useEffect, MouseEvent } from 'react'
import { DataGrid, GridColDef, GridRowSelectionModel, GridRowId } from '@mui/x-data-grid'
import { CircularProgress } from '@mui/material';
import Box from '@mui/material/Box';
import configData from "../config.json";
import { Link } from 'react-router-dom';
import { getUser } from './CommonFunctions';
import { PayPalScriptProvider, PayPalButtons } from "@paypal/react-paypal-js";
import { amountWithBreakdown, items, payee, purchaseUnit } from "../classes/Entities"

function Message({ content }) {
    console.log("Content: " + content);
    return <p>{content}</p>;
}

const columns: GridColDef[] = [

    //{
    //    field: 'salesShoppingCartId', headerName: 'ID', headerClassName: 'header', width: 90, disableColumnMenu: true,
    //},
    { field: 'itemId', headerName: 'Item Id', headerClassName: 'header', flex: 1 },
    { field: 'artistName', headerName: 'Artist', headerClassName: 'header', flex: 1 },
    { field: 'albumName', headerName: 'Record', headerClassName: 'header', flex: 1 },
    { field: 'salesStatusValue', headerName: 'Status', headerClassName: 'header', flex: 1 },
    { field: 'albumOwnerName', headerName: 'Seller', headerClassName: 'header', flex: 1 },
    {
        field: 'price', headerName: 'Price', headerClassName: 'header', flex: 1,
        valueGetter: (value) => {
            if (!value) {
                return value;
            }
            // Convert the decimal value to a percentage
            return parseFloat(value).toFixed(2);
        }
    },
]
//valueSetter: ${ parseFloat(album['price']).toFixed(2)},
function ShoppingCart() {
    const user = getUser();
    const [tableData, setTableData] = useState([])
    const [loading, setLoading] = useState(false);
    const [subTotal, setSubtotal] = useState(0.00);
    const [totalCost, setTotalCost] = useState(0.00);
    const [purchaseArray, setPurchaseArray] = useState([]);
    const [sellers, setSellers] = useState([]);
    const [selectionModel, setSelectionModel] = useState<GridRowSelectionModel | undefined | number>({ type: 'include', ids: new Set<GridRowId>() });

    useEffect(() => {
        setLoading(true);
        fetch(configData.SERVER_URL + 'Sales/GetShoppingCartByUser/' + user.userId)
            .then((data) => data.json())
            .then((data) => {
                setTableData(data);
                setLoading(false);

                //Set total cost based on price of items in shopping cart
                let tempTotal = 0.00;
                const checkedUnits: [] = [];
                const checkedSeller: [] = [];
                data.forEach((eachItem: object) => {
                    tempTotal = tempTotal + eachItem.price;
                    checkedUnits.push(eachItem);
                    if (!checkedSeller.includes(eachItem.albumOwnerId)) {
                        checkedSeller.push(eachItem.albumOwnerId);
                    }
                });
                console.log("checkedSellers: " + checkedSeller);
                setSubtotal(tempTotal);
                console.log("Total: " + (tempTotal + tempTotal * .07))
                setTotalCost(tempTotal + tempTotal * .07)
                console.log(checkedUnits);
                setPurchaseArray(checkedUnits);
                setSellers(checkedSeller);
                //Set checkboxes for each itam in the shopping cart
                //const mixedArray: (string | Set<GridRowId> | GridRowId | number)[] = [{ type: 'include', ids: new Set<GridRowId>() }, 0];
                const mixedArray: (GridRowSelectionModel | undefined | number)[] = [{ type: 'include', ids: new Set<GridRowId>() }];
                for (let x = 1; x < data.length + 1; x++) {
                    mixedArray.push(x);
                }
                console.log("mixedArray: " + mixedArray)
                setSelectionModel(mixedArray);
                
            });

    }, [])

    const initialOptions = {
        "client-id": "test",
        "clientId": "test",
        "enable-funding": "venmo",
        "disable-funding": "",
        "buyer-country": "US",
        currency: "USD",
        "data-page-type": "product-details",
        components: "buttons",
        "data-sdk-integration-source": "developer-studio",
    };

    const [message, setMessage] = useState("");

    // Steps to make the payment
    // 1) create Order in DB
    // 2) send request to PayPal with reference id  and custom id = order id
    // 3) Send Capture
    // 4) if Capture successful
            // a) update orders table with PayPal order ID
            // b) insert payemnt info into Payment table

    //objects and related fields for PayPal can be found in the Entities.ts file
   
   

    const buildPurchaseUnit = (genId: number, root: purchaseUnit[],sellersArray: number[]) => {

        // Create a separate purchaseUnit for each owner usin the sellersArray and calling this function recursively
        console.log("Sellers: " + sellers.length)
        console.log("sellersArray : " + sellersArray[0])
        console.log("purchaseArray: " + purchaseArray.length);

        if (sellersArray.length === 0) {
            console.log("Exiting function");
            return;
        }
       
        //purchaseArray
        const itemList = [];
        let currency = "";
        let tax = 0.00;
        let merchantId = "";
        let merchantEmail = "";
        let currentSubTotal = 0;
        let countForShipping = 0;
        for (let x = 0; x < purchaseArray.length; x++) {
            //Get items needed for other objects from the first item in the array
            
            if (x == 0) {
                currency = purchaseArray[x]["currency"];
                //TODO: Build tax rates for different provinces and countries
                tax = purchaseArray[x]["taxRate"];
                merchantId = purchaseArray[x]["payPalMerchantID"];
                merchantEmail = purchaseArray[x]["payPalEmail"];
            }
            if (sellersArray[0] != purchaseArray[x]["albumOwnerId"]) {
                continue;
            }
            const newItem = new items();
            newItem.itemCost = purchaseArray[x]["price"];
            newItem.itemDescription = purchaseArray[x]["albumDescription"];
            newItem.itemName = purchaseArray[x]["albumName"];
            itemList.push(newItem);
            currentSubTotal = currentSubTotal + purchaseArray[x]["price"];
            countForShipping++;
        }
        
        const objAmount = new amountWithBreakdown();
        objAmount.currency = currency;
        //TODO: Get shipping rate from Canada Post using addresses. Google weight of an album, cd and ?
        objAmount.shipping = 15 + ((countForShipping - 1) * 5);
        //round tax to 2 decimal places
        const factor = 10 ** 2;
        objAmount.tax = Math.round((currentSubTotal * tax) * factor) / factor; //(subTotal * tax).toFixed(2); 
        objAmount.totalCost = currentSubTotal + objAmount.tax;

        const objPayee = new payee();
        objPayee.merchantEmail = merchantEmail;
        objPayee.merchantId = merchantId;

        const objNew = new purchaseUnit();
        objNew.referenceId = genId;
        objNew.customId = genId;
        objNew.amount = objAmount;
        objNew.payee = objPayee;
        objNew.items = itemList;

        root.push(objNew);
        sellersArray.splice(0, 1);
        buildPurchaseUnit(2, root, sellersArray);

        const resultJSON = JSON.stringify(root);
        return resultJSON;
    }
    const handleClick = (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        const root: purchaseUnit[] = [];
        const sellersArray: number[] = [];
        for (let x = 0; x < sellers.length; x++) {
            sellersArray.push(sellers[x]);
        }
        const resultJSON = buildPurchaseUnit(1, root, sellersArray);
        console.log(resultJSON);
    }
    
    if (loading) {

        return <CircularProgress style={{ color: "gold" }} className="circular" />;
    }
    else {
        return (
            <div className="gridPanel">

                <Box sx={{ '& .MuiDataGrid-root': { border: 0 } }} style={{ borderRadius: 25, top: 100 }}>
                    <DataGrid
                        rows={tableData}
                        columns={columns}
                        getRowId={(row) => row.salesShoppingCartId}
                        checkboxSelection
                        keepNonExistentRowsSelected 
                        onRowSelectionModelChange={(id) => {
                            const selectedIDs = new Set(id);
                            console.log(id)
                            console.log(selectedIDs)
                            const selectedRowData = tableData.filter((row) =>
                                selectedIDs.has(row['salesShoppingCartId'])
                            );
                            let tempTotal = 0.00;
                            const checkedUnits: [] = [];
                            const checkedSeller: [] = [];
                            selectedRowData.forEach((eachItem) => {
                                console.log(eachItem['itemId']);
                                tempTotal = tempTotal + eachItem['price'];
                                checkedUnits.push(eachItem);
                                if (!checkedSeller.includes(eachItem['albumOwnerId'])) {
                                    checkedSeller.push(eachItem['albumOwnerId']);
                                }
                            });
                            setSubtotal(tempTotal);
                            console.log("Total: " + (tempTotal + tempTotal * .07))
                            setTotalCost(tempTotal + tempTotal * .07)
                            console.log(selectedRowData); //prints object
                            setSelectionModel(id);
                            console.log(checkedUnits);
                            setPurchaseArray(checkedUnits);
                            setSellers(checkedSeller);
                            
                        }}
                        rowSelectionModel={selectionModel}
                        
                        initialState={{
                            pagination: { paginationModel: { pageSize: 10 } },
                        }}
                        getRowHeight={() => "auto"}
                        pageSizeOptions={[5, 10, 25]}
                        autoHeight {...tableData}

                        sx={{
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
                            '& .MuiCheckbox-root.Mui-checked': {
                                color: 'white'
                            },
                            '& .MuiCheckbox-root': {
                                color: 'white'
                            },
                            '& .MuiDataGrid-columnHeaderTitleContainer': {
                                backgroundColor: 'black',
                                color: 'white',
                                ".MuiSvgIcon-root": {
                                    color: "white",
                                }
                            }
                        }}
                    />
                
                <div className="blockDiv">
                    <div className="flexParentRight">
                            <div className="flex-li-left2">
                            <label>Subtotal:</label>
                        </div>
                            <div className="flex-li-right2">
                                <label>${(Math.round(subTotal * 100) / 100).toFixed(2)}</label>
                        </div>
                    </div>
                    <div className="flexParentRight">
                            <div className="flex-li-left2">
                            <label>Tax:</label>
                        </div>
                            <div className="flex-li-right2">
                                <label>${(subTotal * .07).toFixed(2)}</label>
                        </div>
                    </div>
                    <div className="flexParentRight">
                            <div className="flex-li-left2">
                            <label>Total:</label>
                        </div>
                            <div className="flex-li-right2">
                                <label>${totalCost}</label>
                        </div>
                    </div>
                
                        <div className="createDiv" >
                             <button onClick={handleClick}>Click</button>
                    <div className="createLink">
                                {/*<Link type="button" className="createLink" to="/artist/add/0">Pay</Link>*/}
                               {/* <button onClick={handleClick}>Click</button>*/}
                    </div>
                            <PayPalScriptProvider options={initialOptions}>
                                <PayPalButtons
                                    style={{
                                        shape: "rect",
                                        layout: "vertical",
                                        color: "gold",
                                        label: "paypal",
                                    }}
                                    createOrder={async () => {
                                        try {
                                            const response = await fetch(configData.SERVER_URL + "Checkout/orders", {
                                                method: "POST",
                                                headers: {
                                                    "Content-Type": "application/json",
                                                    'Accept': 'application/json'
                                                },
                                                // use the "body" param to optionally pass additional order information
                                                // like product ids and quantities
                                                body: JSON.stringify({
                                                    cart: [
                                                        {
                                                            id: "1",
                                                            quantity: "1",
                                                            totalCost: totalCost,
                                                            currency: initialOptions.currency,
                                                            //items: {items}
                                                        },
                                                        //{
                                                        //    id: "2",
                                                        //    quantity: "1",
                                                        //    totalCost: totalCost,
                                                        //    currency: initialOptions.currency,
                                                        //    //items: {items}
                                                        //},
                                                    ],
                                                }),
                                            });

                                            const orderData = await response.json();

                                            if (orderData.id) {
                                                setMessage("orderData.id: " + orderData.id);
                                                return orderData.id;
                                            } else {
                                                const errorDetail = orderData?.details?.[0];
                                                const errorMessage = errorDetail
                                                    ? `${errorDetail.issue} ${errorDetail.description} (${orderData.debug_id})`
                                                    : JSON.stringify(orderData);

                                                throw new Error(errorMessage);
                                            }
                                        } catch (error) {
                                            console.error(error);
                                            setMessage(
                                                `Could not initiate PayPal Checkout...${error}`
                                            );
                                        }
                                    }}
                                    onApprove={async (data, actions) => {
                                        try {
                                            const response = await fetch(
                                                configData.SERVER_URL + `Checkout/orders/${data.orderID}/capture`,
                                                {
                                                    method: "POST",
                                                    headers: {
                                                        "Content-Type": "application/json",
                                                        'Accept': 'application/json'
                                                    },
                                                }
                                            );

                                            const orderData = await response.json();
                                            // Three cases to handle:
                                            //   (1) Recoverable INSTRUMENT_DECLINED -> call actions.restart()
                                            //   (2) Other non-recoverable errors -> Show a failure message
                                            //   (3) Successful transaction -> Show confirmation or thank you message

                                            const errorDetail = orderData?.details?.[0];

                                            if (errorDetail?.issue === "INSTRUMENT_DECLINED") {
                                                // (1) Recoverable INSTRUMENT_DECLINED -> call actions.restart()
                                                // recoverable state, per https://developer.paypal.com/docs/checkout/standard/customize/handle-funding-failures/
                                                return actions.restart();
                                            } else if (errorDetail) {
                                                // (2) Other non-recoverable errors -> Show a failure message
                                                throw new Error(
                                                    `${errorDetail.description} (${orderData.debug_id})`
                                                );
                                            } else {
                                                // (3) Successful transaction -> Show confirmation or thank you message
                                                // Or go to another URL:  actions.redirect('thank_you.html');
                                                const transaction =
                                                    orderData.purchase_units[0].payments
                                                        .captures[0];
                                                setMessage(
                                                    `Transaction ${transaction.status}: ${transaction.id}. See console for all available details`
                                                );
                                                console.log(
                                                    "Capture result",
                                                    orderData,
                                                    JSON.stringify(orderData, null, 2)
                                                );
                                            }
                                        } catch (error) {
                                            console.error(error);
                                            setMessage(
                                                `Sorry, your transaction could not be processed...${error}`
                                            );
                                        }
                                    }}
                                />
                            </PayPalScriptProvider>
                            <Message content={message} />
                </div>
                 </div>   
                </Box>
            </div>
        )
    }
}
export default ShoppingCart;