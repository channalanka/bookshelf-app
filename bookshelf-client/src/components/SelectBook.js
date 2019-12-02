import React from 'react';
import {connect} from 'react-redux';
import {loanBook,returnBook} from "../actions/middleware";


const SelectedBook =({book, loanBook, returnBook}) =>{
    if(book){
        return <div>
            <div>{book.title}</div>
            <div>{book.author}</div>
            <div>{book.isbn}</div>
            {book.loanedToId ?(
                <button className="ui inverted orange button" onClick={()=> returnBook({
                    BookId: book.bookId,
                    Isbn: book.isbn
                    })
                }>Return</button>
            ) : (
                <button className="ui inverted purple button" onClick={()=> loanBook({
                    BookId: book.bookId,
                    Isbn: book.isbn
                    })
                }>Loan</button>
            )
            }
        </div>

    }else {
        return <div>No selected book</div>
    }
}
   
    
   



const mapStateToProps= (state)=>{
    return {book: state.bookshelf.selectedBook};
}

export default connect(mapStateToProps,
    {
        loanBook,
        returnBook
    }
    )(SelectedBook);