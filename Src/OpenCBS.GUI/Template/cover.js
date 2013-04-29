function mouse_enter_book(book_node)
{
	 if (book_node.className != 'list_content_clicked')
	 {
		   book_node.className='list_content_enter';
	 }
}

function mouse_leave_book(book_node)
{
    if (book_node.className != 'list_content_clicked')
    {
        book_node.className='list_content';
    }
}

var previous_selected_book;

function click_book(book_node,package_id)
{
    if(previous_selected_book != null)
    {
        if(previous_selected_book != book_node)
        {
            previous_selected_book.className = 'list_content';
        }
    }
    
    if (book_node.className != 'list_content_clicked')
    {
        book_node.className='list_content_clicked';
        previous_selected_book = book_node;
    }
    else
    {
        book_node.className='list_content';
        previous_selected_book = null;
    }
}
