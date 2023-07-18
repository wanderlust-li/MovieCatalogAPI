import React from 'react';
import logo from '../../images/react.png';

function Header() {
    return (
        <nav className="navbar navbar-expand-lg navbar-dark" style={{ backgroundColor: '#141414' }}>
            <a className="navbar-brand" href="#">
                <img src={logo} alt="Netflix Logo" height="60" />
            </a>
            <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarContent"
                    aria-controls="navbarContent" aria-expanded="false" aria-label="Toggle navigation">
                <span className="navbar-toggler-icon"></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarContent">
                <ul className="navbar-nav ml-auto">
                    <li className="nav-item">
                        <a className="nav-link" href="#" style={{ color: '#E5E5E5' }}>Home</a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link" href="#" style={{ color: '#E5E5E5' }}>TV Shows</a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link" href="#" style={{ color: '#E5E5E5' }}>Movies</a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link" href="#" style={{ color: '#E5E5E5' }}>Recently Added</a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link" href="#" style={{ color: '#E5E5E5' }}>My List</a>
                    </li>
                </ul>
            </div>
        </nav>
    )
}

export default Header;
