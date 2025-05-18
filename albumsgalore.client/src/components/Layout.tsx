import * as React from 'react';
import ReactTab from './ReactTab';
import Header from './Header';

export interface LayoutProps {
    children?: React.ReactNode;
}
export class Layout extends React.Component<LayoutProps> {
    
render() {
   
        return (
            <div >
                <Header />

                <div>
                    <ReactTab />

                    <div className="container-side-by-side">
                        <div className='flex-child-left'>
                            {this.props.children} 
                        </div>
                        <div className='flex-child-right'>
                            <ul>
                                <h2>News</h2>
                                <li>
                                    <a href="https://www.wired.com/tag/vinyl/" target="_blank">News from Vinyl</a>
                                </li>
                                <li>
                                    <a href="https://consequence.net/tag/vinyl/page/2/" target="_blank">Consequence - Latest News</a>
                                </li>
                            </ul>
                            <ul>
                                <h2>Events</h2>
                                <li>
                                    <a href="https://www.marywinspear.ca/event/south-island-vinyl-record-show" target="_blank">South Island Vinyl Record Show</a>
                                </li>
                                <li>
                                    <a href="https://www.facebook.com/p/Vancouver-Vinyl-Record-Show-100069580515019/" target="_blank">Vancouver Vinyl Record Show</a>
                                </li>
                                <li>
                                    <a href="https://www.facebook.com/Vinylrecordfair/events/?paipv=0&eav=Afb7QdpMuJDzYrF6fdIHVjLPWD6VHGuEcV6qv_rTjl1t7LeAyNdRd8fLgOeXW8WGCZg&_rdr" target="_blank">Main Street Vinyl Record Fair</a>
                                </li>
                            </ul>
                        </div> 
                    </div>
                    </div>
                
            </div >
        )
    }
}