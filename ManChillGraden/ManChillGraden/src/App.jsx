import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Header from './components/Header.jsx';
import Footer from './components/Footer.jsx';
import Home from './pages/Home.jsx';
import About from './pages/About.jsx';
import Service from './pages/Service.jsx';
import Menu from './pages/Menu.jsx';
import Booking from './pages/Booking.jsx';
import OurTeam from './pages/OurTeam.jsx';
import Testimonial from './pages/Testimonial.jsx';
import Contact from './pages/Contact.jsx';
import BookTable from './pages/BookTable.jsx';
import 'bootstrap-icons/font/bootstrap-icons.css';
import './style.css';

const App = () => {
    return (
        <Router>
            <div className="container">
                <div className="layout">
                    <Header />
                    <main>
                        <Routes>
                            <Route path="/" element={<Home />} />
                            <Route path="/about" element={<About />} />
                            <Route path="/service" element={<Service />} />
                            <Route path="/menu" element={<Menu />} />
                            <Route path="/booking" element={<Booking />} />
                            <Route path="/our-team" element={<OurTeam />} />
                            <Route path="/testimonial" element={<Testimonial />} />
                            <Route path="/contact" element={<Contact />} />
                            <Route path="/book-table" element={<BookTable />} />
                        </Routes>
                    </main>
                    <Footer />
                </div>
            </div>
        </Router>
    );
};

export default App;
