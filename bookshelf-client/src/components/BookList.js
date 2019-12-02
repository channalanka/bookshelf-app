import React from 'react';
import {connect} from 'react-redux';
import {selectBook} from '../actions';
import {fetchBooks} from "../actions/middleware";

class SongList extends React.Component{
    render(){
        return (
        <div className="ui devided list">
               {this.renderBookList()}
        </div>
        );
    }
    componentDidMount(){
        this.props.fetchBookAction();
    }

    renderBookList(){
        
        return this.props.books.map(x =>{
            return(
                <div className="item" key={x.bookId}>
                    <div className="right floated content">{
                       
                            <button onClick={() => this.props.selectBookAction(x)} 
                            className="ui inverted primary button">
                                 {x.loanedToId?("Return"):("Loan")}
                            </button>
                        
                    }
                    </div>
                    <div className="header">{x.title}</div>
                    <div className="content">
                    {x.author}
                    </div>
                </div>
            );
        })
    }
}


const mapStateToProps= (state)=>{
    return {books: state.bookshelf.books};
}

export default connect(
        mapStateToProps,
        {
            selectBookAction: selectBook,
            fetchBookAction:fetchBooks
        }
    )(SongList);
