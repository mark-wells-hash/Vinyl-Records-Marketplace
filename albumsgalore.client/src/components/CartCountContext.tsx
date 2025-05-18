import React, { useState, SetStateAction, Dispatch } from "react"

interface Children {
    children?: React.ReactNode;
}
interface CartContextType {
    count: number;
    setCount: Dispatch<SetStateAction<number>> //(count: number) => void;
}
//Dispatch<SetStateAction<number>>; //
//interface ContextType {
//    activeNote: {
//        count: number
//    }
//}

//const initialValues = {
//    activeNote: { count: 0 }
//}
//const [salesCount, setSalesCount] = useState(0);
//const UserContext = React.createContext(0)

export const UserContext = React.createContext<CartContextType>({
    count: 0,
    setCount: () => { }
});

export const ContextProvider = ({ children }: Children) => {
    const [count, setCount] = useState(0);
    //setCount: (count: number) => void
    //const [count, setCount ] = useState(5);
    setCount(8)
    //const value = { count, setCount }
    //setValue: (value: string) => void;
    return (
        <UserContext.Provider value={{ count, setCount }}>
            {children}
        </UserContext.Provider>
    )
}

//const CartCountProvider: React.FC = ({ children }: Children) => {
//    const [count, setCount] = React.useState<CartContextType>({
//        count: 0,
//        updateCount: (count: number) => setCount({ count }),
//    });

//    /*const [count, setCount] = React.useState<ContextType>({ count: 1 })*/
       

//    return (
//        <UserContext.Provider value={count}>
//            {children}
//        </UserContext.Provider>
//    );
//};