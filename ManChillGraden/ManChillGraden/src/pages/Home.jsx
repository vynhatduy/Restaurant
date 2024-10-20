import React from 'react';
import Servicecpn from '../components/Servicecpn';
import Aboutcpn from '../components/Aboutcpn';
import TieuDeMenu from '../components/TieuDeMenu';
import TeamStartcpn from '../components/TeamStartcpn';
import FeetBack from '../components/FeetBackcpn';
import ProductList from '../components/Productcpn'
const Home = () => {
  return (
    <div className="home">
      <Servicecpn></Servicecpn>
      <Aboutcpn></Aboutcpn>
      <TieuDeMenu></TieuDeMenu>
      <ProductList />
      <TeamStartcpn></TeamStartcpn>
      <FeetBack></FeetBack>
    </div>
  );
};

export default Home;
