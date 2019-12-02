import React from 'react'
import { connect } from 'react-redux';

import BookList from './BookList';
import SelectedBook from './SelectBook';


class Container extends React.Component {
    render() {
        const {bookshelf} = this.props;

        return (
            <div className="ui placeholder segment">
                <div className="ui icon header">Books Return/Loan</div>
                <br />

                <div className="ui two column stackable center aligned grid">
                    <div className="ui vertical divider"></div>
                    <div className="row">
                        <div className="column eight wide left aligned">
                            <BookList />
                        </div>
                        <div className="column eight wide middle aligned ">
                            { bookshelf.selectedBook 
                            && bookshelf.selectedBook.loanedToId 
                            && bookshelf.selectedBook.loanedToId !== this.props.auth.userId  
                            && <div className="ui warning message">
                                <i className="close icon"></i>
                                <div className="header">
                                  Warning
                                </div>
                                This book has been loaned by some other user
                                </div> }
                            {bookshelf.error && <div className="ui negative message">
                                <i className="close icon"></i>
                                <div className="header">
                                    Error
                                </div>
                                <p>{bookshelf.error}</p>
                            </div>
                            }
                            <SelectedBook />
                        </div>
                    </div>
                </div>
            </div>

        );
    }
}


const mapStateToProps = ({ auth, bookshelf }) => {
    return { auth: auth, bookshelf: bookshelf };
}

export default connect(mapStateToProps)(Container);