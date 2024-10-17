import React from 'react';
import Servicecpn from '../components/Servicecpn';
import Aboutcpn from '../components/Aboutcpn';
import Menucpn from '../components/Menucpn';
import TeamStartcpn from '../components/TeamStartcpn';
import FeetBack from '../components/FeetBackcpn';
const Home = () => {
  return (
    <div className="home">
      <Servicecpn></Servicecpn>
      <Aboutcpn></Aboutcpn>
      <Menucpn></Menucpn>
      <TeamStartcpn></TeamStartcpn>
      <FeetBack></FeetBack>
    </div>
  );
};

export default Home;
