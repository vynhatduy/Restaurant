import React from "react";
import about1 from "../assets/img/about-1.jpg";
import about2 from "../assets/img/about-2.jpg";
import about3 from "../assets/img/about-3.jpg";
import about4 from "../assets/img/about-4.jpg";
const Aboutcpn = () => {
    return (
        <div class="container-xxl py-5">
            <div class="container">
                <div class="row g-5 align-items-center">
                    <div class="col-lg-6">
                        <div class="row g-3">
                            <div class="col-6 text-start">
                                <img class="img-fluid rounded w-100 wow zoomIn" data-wow-delay="0s" src={about1}/>
                            </div>
                            <div class="col-6 text-start">
                                <img class="img-fluid rounded w-75 wow zoomIn" data-wow-delay="0.3s" src={about2}/>
                            </div>
                            <div class="col-6 text-end">
                                <img class="img-fluid rounded w-75 wow zoomIn" data-wow-delay="0.5s" src={about3}/>
                            </div>
                            <div class="col-6 text-end">
                                <img class="img-fluid rounded w-100 wow zoomIn" data-wow-delay="0.7s" src={about4}/>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <h5 class="section-title ff-secondary text-start text-warning fw-normal">About Us</h5>
                        <h1 class="mb-4">Welcome to <i class="fa fa-utensils text-warning me-2"></i>Mận Chill Garden</h1>
                        <p class="mb-4">Chào mừng bạn đến với Mận Chill Garden- nơi hòa mình vào không gian lãng mạn, ấm áp và thưởng thức những món ăn ngon. Mận Chill Garden là điểm đến lý tưởng cho những buổi hẹn hò, gặp gỡ bạn bè, các buổi tiệc sinh nhật, hay đơn giản là bữa ăn gia đình.</p>
                        <div class="row g-4 mb-4">
                            <div class="col-sm-6">
                                <div class="d-flex align-items-center border-start border-5 border-warning px-3">
                                    <h1 class="flex-shrink-0 display-5 text-warning mb-0" data-toggle="counter-up">15</h1>
                                    <div class="ps-4">
                                        <p class="mb-0">Years of</p>
                                        <h6 class="text-uppercase mb-0">Experience</h6>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="d-flex align-items-center border-start border-5 border-warning px-3">
                                    <h1 class="flex-shrink-0 display-5 text-warning mb-0" data-toggle="counter-up">50</h1>
                                    <div class="ps-4">
                                        <p class="mb-0">Popular</p>
                                        <h6 class="text-uppercase mb-0">Master Chefs</h6>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <a class="btn btn-primary py-3 px-5 mt-2" href="">Read More</a>
                    </div>
                </div>
            </div>
        </div>
    );
}; export default Aboutcpn;
