import { Link, NavLink } from 'react-router-dom';
import './frame.css';
import logo from '../assets/logo/logo.png';
import hero from '../assets/img/hero.png';
import bg_hero from '../assets/img/bg-hero.jpg';
import React, { useState, useEffect } from 'react';

const Header = () => {
    const [isScrolled, setIsScrolled] = useState(false);

    // Xử lý sự kiện cuộn trang
    useEffect(() => {
        const handleScroll = () => {
            setIsScrolled(window.scrollY > 60); 
        };

        window.addEventListener('scroll', handleScroll);

        return () => {
            window.removeEventListener('scroll', handleScroll);
        };
    }, []);

    return (
        <header>
            <div
                className="top-hero"
                style={{
                    background: `linear-gradient(rgba(15, 23, 43, .9), rgba(15, 23, 43, .9)), url(${bg_hero})`,
                    backgroundRepeat: 'no-repeat',
                    backgroundSize: 'cover',
                    backgroundPosition: 'center'
                }}
            >
                <div className={`top ${isScrolled ? 'sticky' : ''}`}>
                    <img className="logo" src={logo} alt="ManChillGraden" width="200px" height="200px" />
                    <h1 className="name">Mận Chill Garden</h1>
                    <nav>
                        <ul>
                            <li><NavLink exact to="/" activeClassName="active">Home</NavLink></li>
                            <li><NavLink to="/about" activeClassName="active">About</NavLink></li>
                            <li><NavLink to="/service" activeClassName="active">Service</NavLink></li>
                            <li><NavLink to="/menu" activeClassName="active">Menu</NavLink></li>
                            <li className="pages dropdown-toggle text-white">
                                PAGES
                                <ul className="submenu">
                                    <li><NavLink to="/booking" activeClassName="active">Booking</NavLink></li>
                                    <li><NavLink to="/our-team" activeClassName="active">Our Team</NavLink></li>
                                    <li><NavLink to="/testimonial" activeClassName="active">Testimonial</NavLink></li>
                                </ul>
                            </li>
                            <li><NavLink to="/contact" activeClassName="active">Contact</NavLink></li>
                            
                            <li><NavLink to="/book-table" activeClassName="active" className="btn btn-primary">Book A Table</NavLink></li>
                        </ul>
                    </nav>
                </div>
                <div className="middle">
                    <div className="left">
                        <h1 className='text-white'>Enjoy Our Delicious Meal</h1>
                        <p class="text-white">Chào mừng bạn đến với Mận Chill Garden- nơi hòa mình vào không gian lãng mạn, ấm áp và thưởng thức những món ăn ngon. Mận Chill Garden là điểm đến lý tưởng cho những buổi hẹn hò, gặp gỡ bạn bè, các buổi tiệc sinh nhật, hay đơn giản là bữa ăn gia đình.</p>
                        <a className="btn btn-primary">Book A Table</a>
                    </div>
                    <div className="right">
                        <img className="img-head" src={hero} width="400px" height="400px" alt="Hero" />
                    </div>
                </div>
            </div>
        </header>
    );
};

export default Header;
