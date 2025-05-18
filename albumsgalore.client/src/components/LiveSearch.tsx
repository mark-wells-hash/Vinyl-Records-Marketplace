import { FC, useCallback, useEffect, useRef, useState } from "react";

interface Props<T> {
    results?: T[];
    renderItem(item: T): JSX.Element;
    renderItem2(item: T): JSX.Element;
    onChange?: React.ChangeEventHandler;
    onSelectA?: (item: T) => void;
    onSelectB?: (item: T) => void;
    value?: string;
    placeholder?: string;
    widthIn?: string|number;
    heightIn?: string;
}

const LiveSearch = <T extends object>({
    results = [],
    renderItem,
    renderItem2,
    value,
    onChange,
    onSelectA,
    onSelectB,
    placeholder,
    widthIn,
    heightIn,
}: Props<T>): JSX.Element => {
    const [focusedIndex, setFocusedIndex] = useState(-1);
    const resultContainer = useRef<HTMLDivElement>(null);
    const [showResults, setShowResults] = useState(false);
    const [defaultValue, setDefaultValue] = useState("");

    const handleSelectionA = (selectedIndex: number) => {
        const selectedItem = results[selectedIndex];
        if (!selectedItem) return resetSearchComplete();
        onSelectA && onSelectA(selectedItem);
        resetSearchComplete();
    };

    const handleSelectionB = (selectedIndex: number) => {
        const selectedItem = results[selectedIndex];
        if (!selectedItem) return resetSearchComplete();
        onSelectB && onSelectB(selectedItem);
        resetSearchComplete();
    };

    const resetSearchComplete = useCallback(() => {
        setFocusedIndex(-1);
        setShowResults(false);
        setDefaultValue("");
    }, []);

    const handleKeyDown: React.KeyboardEventHandler<HTMLDivElement> = (e) => {
        const { key } = e;
        let nextIndexCount = 0;

        // move down
        if (key === "ArrowDown")
            nextIndexCount = (focusedIndex + 1) % results.length;

        // move up
        if (key === "ArrowUp")
            nextIndexCount = (focusedIndex + results.length - 1) % results.length;

        // hide search results
        if (key === "Escape") {
            resetSearchComplete();
        }

        // select the current item
        if (key === "Enter") {
            e.preventDefault();
            handleSelectionA(focusedIndex);
        }

        setFocusedIndex(nextIndexCount);
    };

    type changeHandler = React.ChangeEventHandler<HTMLInputElement>;
    const handleChange: changeHandler = (e) => {
        setDefaultValue(e.target.value);
        onChange && onChange(e);
    };

    useEffect(() => {
        if (!resultContainer.current) return;

        resultContainer.current.scrollIntoView({
            block: "center",
        });
    }, [focusedIndex]);

    useEffect(() => {
        if (results.length > 0 && !showResults) setShowResults(true);

        if (results.length <= 0) setShowResults(false);
    }, [results, showResults]);

    useEffect(() => {
        if (value) setDefaultValue(value);
    }, [value]);

    return (
        <div className="h-screen flex items-center justify-center">
            <div
                tabIndex={1}
                onBlur={resetSearchComplete}
                onKeyDown={handleKeyDown}
                className="relative"
            >
                <input
                    value={defaultValue}
                    onChange={handleChange}
                    type="text"
                    className=""
                    placeholder={placeholder}
                    style={{ width: widthIn, height: heightIn }}
                />

                {/* Search Results Container */}
                {showResults && (
                    <div className="absolute mt-1 w-full p-2 bg-white shadow-lg rounded-bl rounded-br max-h-56 overflow-y-auto">
                        <table>
                            <tr>
                                <td style={{ width: '150px', fontWeight: 700 }}>Artist</td>
                                <td style={{ width: '200px', fontWeight: 700 }}>Album</td>
                            </tr>
                        </table>
                        <hr/>
                        {results.map((item, index) => {
                            return (
                                <table>
                                    <tr>
                                        <td style={{ width: '150px'}}>
                                            <div
                                                key={index}
                                                onMouseDown={() => handleSelectionA(index)}
                                                ref={index === focusedIndex ? resultContainer : null}
                                                style={{ width: widthIn, height: heightIn, paddingLeft: "10px" }}
                                                className="searchHover" //cursor-pointer hover:bg-black hover:bg-opacity-10 p-2
                                            >
                                                
                                                {renderItem(item)}
                                            </div>
                                        </td>
                                        <td style={{ width: '200px' }}>    
                                             <div
                                                key={index}
                                                onMouseDown={() => handleSelectionB(index)}
                                                ref={(index) === focusedIndex ? resultContainer : null}
                                                style={{ width: widthIn, height: heightIn, paddingLeft: "10px" }}
                                                className="searchHover" //cursor-pointer hover:bg-black hover:bg-opacity-10 p-2
                                                >
                                    
                                                {renderItem2(item)}
                                              </div>
                                        </td>
                                    </tr>
                                </table>
                            );
                        })}
                    </div>
                )}
            </div>
        </div>
    );
};

export default LiveSearch;