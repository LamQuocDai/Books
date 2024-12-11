import { useState, useEffect } from "react";
import Header from "./Header";

function HomePage() {
  const [books, setBooks] = useState([]);
  const [bookTypes, setBookTypes] = useState([]);
  const [authors, setAuthors] = useState([]);
  const [users, setUsers] = useState([]);
  const [isEditBook, setIsEditBook] = useState(null);
  const [isEditUser, setIsEditUser] = useState(null);
  const [dataChange, setDataChange] = useState(false);
  const [editBook, setEditBook] = useState({
    name: "",
    price: "",
    authorId: "",
    bookTypeId: "",
    releaseDate: "",
  });
  const [editUser, setEditUser] = useState({
    name: "",
    phone: "",
  });

  // add new book and user
  const [isAddBook, setIsAddBook] = useState(false);
  const [isAddUser, setIsAddUser] = useState(false);
  const [newBook, setNewBook] = useState({
    name: "",
    price: "",
    authorId: "",
    bookTypeId: "",
    releaseDate: "",
  });
  const [newUser, setNewUser] = useState({
    name: "",
    email: "",
    phone: "",
    password: "",
  });

  // Fetch data from API
  useEffect(() => {
    // Fetch books into state (books)
    const fetchBooks = async () => {
      try {
        const response = await fetch("https://localhost:7109/api/books");
        if (!response.ok) {
          throw new Error("Fetch books failed");
        }
        const data = await response.json();
        setBooks(data);
      } catch (error) {
        console.error("Fetch books error: ", error);
      }
    };
    fetchBooks();

    // Fetch type books into state (bookTypes)
    const fetchBookTypes = async () => {
      try {
        const response = await fetch("https://localhost:7109/api/booktypes");
        if (!response.ok) {
          throw new Error("Fetch type books failed");
        }
        const data = await response.json();
        setBookTypes(data);
        setNewBook({ ...newBook, bookTypeId: data[0].id.toString() });
      } catch (error) {
        console.error("Fetch type books error: ", error);
      }
    };
    fetchBookTypes();

    // Fetch authors into state (authors)
    const fetchAuthors = async () => {
      try {
        const response = await fetch("https://localhost:7109/api/authors");
        if (!response.ok) {
          throw new Error("Fetch authors failed");
        }
        const data = await response.json();
        setAuthors(data);
        setNewBook({ ...newBook, authorId: data[0].id.toString() });
      } catch (error) {
        console.error("Fetch authors error: ", error);
      }
    };
    fetchAuthors();

    // Fetch users into state (users)
    const fetchUsers = async () => {
      try {
        const response = await fetch("https://localhost:7109/api/users");
        if (!response.ok) {
          throw new Error("Fetch users failed");
        }
        const data = await response.json();
        setUsers(data);
      } catch (error) {
        console.error("Fetch users error: ", error);
      }
    };
    fetchUsers();
  }, [dataChange]);

  // Start edit book
  const startEditBook = (book) => {
    setIsEditBook(book.id);
    setEditBook(book);
  };

  // Start edit user
  const startEditUser = (user) => {
    setIsEditUser(user.id);
    setEditUser(user);
  };

  // Change property of editBook state
  const handleEditBookChange = (e) => {
    const { name, value } = e.target;
    setEditBook((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  // Change property of editUser state
  const handleEditUserChange = (e) => {
    const { name, value } = e.target;
    setEditUser((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  //
  const handleAddBookChange = (e) => {
    const { name, value } = e.target;
    setNewBook((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  //
  const handleAddUserChange = (e) => {
    const { name, value } = e.target;
    setNewUser((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  // Handle submit edit book
  const handleEditBookSubmit = async () => {
    try {
      const response = await fetch(
        `https://localhost:7109/api/books/${editBook.id}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(editBook),
        }
      );
      if (!response.ok) {
        throw new Error("Edit book failed");
      }
      setDataChange(!dataChange);
      setIsEditBook(null);
    } catch (error) {
      console.error("Edit book error: ", error);
    }
  };

  // Add handleEditUserSubmit function here
  const handleEditUserSubmit = async () => {
    try {
      const response = await fetch(
        `https://localhost:7109/api/users/${editUser.id}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(editUser),
        }
      );
      if (!response.ok) {
        throw new Error("Edit user failed");
      }
      setDataChange(!dataChange);
      setIsEditUser(null);
    } catch (error) {
      console.error("Edit user error: ", error);
    }
  };

  // Handle delete book
  const handleDeleteBook = async (id) => {
    try {
      const response = await fetch(`https://localhost:7109/api/books/${id}`, {
        method: "DELETE",
      });
      if (!response.ok) {
        throw new Error("Delete book failed");
      }
      setDataChange(!dataChange);
    } catch (error) {
      console.error("Delete book error: ", error);
    }
  };

  // Handle delete user
  const handleDeleteUser = async (id) => {
    try {
      const response = await fetch(`https://localhost:7109/api/users/${id}`, {
        method: "DELETE",
      });
      if (!response.ok) {
        throw new Error("Delete user failed");
      }
      setDataChange(!dataChange);
    } catch (error) {
      console.error("Delete user error: ", error);
    }
  };

  // Add handleAddBook function here
  const handleAddBook = async () => {
    try {
      const response = await fetch("https://localhost:7109/api/books", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(newBook),
      });
      if (!response.ok) {
        throw new Error("Add book failed");
      }
      setDataChange(!dataChange);
      setNewBook({
        name: "",
        price: "",
        authorId: authors[0].id.toString(),
        bookTypeId: bookTypes[0].id.toString(),
        releaseDate: "",
      });
      setIsAddBook(false);
    } catch (error) {
      console.error("Add book error: ", error);
    }
  };

  // Add handleAddUser function here
  const handleAddUser = async () => {
    try {
      const response = await fetch("https://localhost:7109/api/users", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(newUser),
      });
      if (!response.ok) {
        throw new Error("Add user failed");
      }
      setDataChange(!dataChange);
      setNewUser({ name: "", email: "", phone: "", password: "" });
      setIsAddUser(false);
    } catch (error) {
      console.error("Add user error: ", error);
    }
  };

  return (
    <>
      <Header />
      <div className="h-full w-full">
        <h1 className="text-4xl">Books</h1>
        <button onClick={() => setIsAddBook(true)} className="bg-blue-600 text-white rounded hover:bg-blue-700 p-2">Add Book</button>
        <div className="border border-gray-300 mt-4">
          <div className="grid grid-cols-7 gap-5 font-bold border-b border-gray-300 p-2">
            <p className="border-r border-gray-300">Name</p>
            <p className="border-r border-gray-300">Price</p>
            <p className="border-r border-gray-300">Author</p>
            <p className="border-r border-gray-300">Type</p>
            <p className="border-r border-gray-300">Release Date</p>
            <p className="border-r border-gray-300">Edit</p>
            <p>Delete</p>
          </div>
          {books.map((book, index) => (
            <div key={index} className="grid grid-cols-7 gap-5 border-b border-gray-300 p-2">
              {isEditBook === book.id ? (
                <>
                  <input
                    type="text"
                    name="name"
                    value={editBook.name}
                    onChange={handleEditBookChange}
                    className="border border-gray-300 p-2"
                  />
                  <input
                    type="text"
                    name="price"
                    value={editBook.price}
                    onChange={handleEditBookChange}
                    className="border border-gray-300 p-2"
                  />
                  <select
                    name="authorId"
                    value={editBook.authorId}
                    onChange={handleEditBookChange}
                    className="border border-gray-300 p-2"
                  >
                    {authors.map((author) => (
                      <option key={author.id} value={author.id}>
                        {author.name}
                      </option>
                    ))}
                  </select>
                  <select
                    name="bookTypeId"
                    value={editBook.bookTypeId}
                    onChange={handleEditBookChange}
                    className="border border-gray-300 p-2"
                  >
                    {bookTypes.map((type) => (
                      <option key={type.id} value={type.id}>
                        {type.name}
                      </option>
                    ))}
                  </select>
                  <input
                    type="date"
                    name="releaseDate"
                    value={editBook.releaseDate}
                    onChange={handleEditBookChange}
                    className="border border-gray-300 p-2"
                  />
                  <input
                    type="button"
                    value="Submit"
                    onClick={handleEditBookSubmit}
                    className="bg-green-500 text-white rounded hover:bg-green-700 p-2"
                  />
                  <input
                    type="button"
                    value="Cancel"
                    onClick={() => setIsEditBook(null)}
                    className="bg-red-500 text-white rounded hover:bg-red-700 p-2"
                  />
                </>
              ) : (
                <>
                  <p className="border border-gray-300 p-2">{book.name}</p>
                  <p className="border border-gray-300 p-2">{book.price}</p>
                  <p className="border border-gray-300 p-2">
                    {
                      authors.find((author) => author.id === book.authorId)
                        ?.name
                    }
                  </p>
                  <p className="border border-gray-300 p-2">
                    {
                      bookTypes.find((type) => type.id === book.bookTypeId)
                        ?.name
                    }
                  </p>
                  <p className="border border-gray-300 p-2">{book.releaseDate}</p>
                  <p>
                    <input
                      type="button"
                      value="Edit"
                      onClick={() => startEditBook(book)}
                      className="bg-yellow-500 text-white rounded hover:bg-yellow-700 p-2"
                    />
                  </p>
                  <p>
                    <input
                      type="button"
                      value="Delete"
                      onClick={() => handleDeleteBook(book.id)}
                      className="bg-red-500 text-white rounded hover:bg-red-700 p-2"
                    />
                  </p>
                </>
              )}
            </div>
          ))}
          <div className="grid grid-cols-7 gap-5">
            {isAddBook && (
              <>
                <input
                  type="text"
                  name="name"
                  value={newBook.name}
                  onChange={handleAddBookChange}
                  placeholder="name"
                  className="border border-gray-300 p-2"
                />
                <input
                  type="text"
                  name="price"
                  value={newBook.price}
                  onChange={handleAddBookChange}
                  placeholder="price"
                  className="border border-gray-300 p-2"
                />
                <select
                  name="authorId"
                  value={newBook.authorId}
                  onChange={handleAddBookChange}
                  className="border border-gray-300 p-2"
                >
                  <option>Select author</option>
                  {authors.map((author) => (
                    <option key={author.id} value={author.id}>
                      {author.name}
                    </option>
                  ))}
                </select>
                <select
                  name="bookTypeId"
                  value={newBook.bookTypeId}
                  onChange={handleAddBookChange}
                  className="border border-gray-300 p-2"
                >
                  <option>Select type book</option>
                  {bookTypes.map((type) => (
                    <option key={type.id} value={type.id}>
                      {type.name}
                    </option>
                  ))}
                </select>
                <input
                  type="date"
                  name="releaseDate"
                  value={newBook.releaseDate}
                  onChange={handleAddBookChange}
                  className="border border-gray-300 p-2"
                />
                <button type="button" onClick={handleAddBook}
                className="bg-green-500 text-white rounded hover:bg-green-700 p-2">
                  Save
                </button>
                <button type="button" onClick={() => setIsAddBook(false)}
                  className="bg-red-500 text-white rounded hover:bg-red-700 p-2">
                  Cancel
                </button>
              </>
            )}
          </div>
        </div>
        <h1 className="text-4xl mt-8 mb-4">Users</h1>
        <button onClick={() => setIsAddUser(true)} className="bg-blue-600 text-white rounded hover:bg-blue-700 p-2">Add User</button>
        <div className="border border-gray-300 mt-4 ">
          <div className="grid grid-cols-5 gap-4 font-bold border-b border-gray-300 p-2">
            <p className="border-r border-gray-300">Name</p>
            <p className="border-r border-gray-300">Email</p>
            <p className="border-r border-gray-300">Phone</p>
            <p className="border-r border-gray-300">Edit</p>
            <p className="border-r border-gray-300">Delete</p>
          </div>
          {users.map((user, index) => (
            <div key={index} className="grid grid-cols-5 gap-4 border-b border-gray-300 p-2">
              {isEditUser === user.id ? (
                <>
                  <input
                    type="text"
                    name="name"
                    value={editUser.name}
                    onChange={handleEditUserChange}
                    className="border border-gray-300 p-2"
                  />
                  <p className="border border-gray-300 p-2">{user.email}</p>
                  <input
                    type="text"
                    name="phone"
                    value={editUser.phone}
                    onChange={handleEditUserChange}
                    className="border border-gray-300 p-2"
                  />
                  <input
                    type="button"
                    value="Submit"
                    onClick={handleEditUserSubmit}
                    className="bg-green-500 text-white rounded hover:bg-green-700 p-2"
                  />
                  <input
                    type="button"
                    value="Cancel"
                    onClick={() => setIsEditUser(null)}
                    className="bg-red-500 text-white rounded hover:bg-red-700 p-2"
                  />
                </>
              ) : (
                <>
                  <p className="border border-gray-300 p-2">{user.name}</p>
                  <p className="border border-gray-300 p-2">{user.email}</p>
                  <p className="border border-gray-300 p-2">{user.phone}</p>
                  <p>
                    <input
                      type="button"
                      value="Edit"
                      onClick={() => startEditUser(user)}
                      className="bg-yellow-500 text-white rounded hover:bg-yellow-700 p-2"
                    />
                  </p>
                  <p>
                    <input
                      type="button"
                      value="Delete"
                      onClick={() => handleDeleteUser(user.id)}
                      className="bg-red-500 text-white rounded hover:bg-red-700 p-2"
                    />
                  </p>
                </>
              )}
            </div>
          ))}
          <div className="grid grid-cols-6 gap-5">
            {isAddUser && (
              <>
                <input
                  type="text"
                  name="name"
                  value={newUser.name}
                  onChange={handleAddUserChange}
                  placeholder="name"
                  className="border border-gray-300 p-2"
                />
                <input
                  type="text"
                  name="email"
                  value={newUser.email}
                  onChange={handleAddUserChange}
                  placeholder="email"
                  className="border border-gray-300 p-2"
                />
                <input
                  type="text"
                  name="phone"
                  value={newUser.phone}
                  onChange={handleAddUserChange}
                  placeholder="phone"
                  className="border border-gray-300 p-2"
                />
                <input
                  type="text"
                  name="password"
                  value={newUser.password}
                  onChange={handleAddUserChange}
                  placeholder="password"
                  className="border border-gray-300 p-2"
                />
                <button
                  type="button"
                  onClick={handleAddUser}
                  className="bg-green-500 text-white rounded hover:bg-green-700 p-2"
                >
                  Save
                </button>
                <button
                  type="button"
                  onClick={() => setIsAddUser(false)}
                  className="bg-red-500 text-white rounded hover:bg-red-700 p-2"
                >
                  Cancel
                </button>
              </>
            )}
          </div>
        </div>
      </div>
    </>
  );
}
export default HomePage;
