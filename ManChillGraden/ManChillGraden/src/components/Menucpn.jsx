import React, { useEffect, useState } from "react";


const Menucpn = () => {

    const [menuItems, setMenuItems] = useState([]);
    useEffect(() => {
        // Hàm gọi API
        const fetchMenuItems = async () => {
            try {
                const response = await fetch('https://localhost:5001/api/Products/AllProduct'); // Thay link API vào đây
                const data = await response.json();
                setMenuItems(data); // Lưu dữ liệu vào state
            } catch (error) {
                console.error("Error fetching menu items:", error);
            }
        };

        fetchMenuItems(); // Gọi hàm
    }, []);
    return (
        <div className="container-xxl py-5">
            <div className="container">
                <div className="text-center wow fadeInUp" data-wow-delay="0.1s">
                    <h5 className="section-title ff-secondary text-center text-primary fw-normal">Food Menu</h5>
                    <h1 className="mb-5">Món ăn phổ biến nhất</h1>
                </div>
                <div className="tab-class text-center wow fadeInUp" data-wow-delay="0.1s">
                    <ul className="nav nav-pills d-inline-flex justify-content-center border-bottom mb-5">
                        {/* Tabs ở đây */}
                    </ul>
                    <div className="tab-content">
                        <div id="tab-1" className="tab-pane fade show p-0 active">
                            <div className="row g-4">
                                {/* Hiển thị danh sách món ăn */}
                                {menuItems.map((item, index) => (
                                    <div className="col-lg-6" key={index}>
                                        <div className="d-flex align-items-center">
                                            {/* <img className="flex-shrink-0 img-fluid rounded" src={item.imageUrl} alt="" style="width: 80px;"> */}
                                            <div className="w-100 d-flex flex-column text-start ps-4">
                                                <h5 className="d-flex justify-content-between border-bottom pb-2">
                                                    <span>{item.tenSanPham}</span>
                                                    <span className="text-primary">${item.donGia}</span>
                                                </h5>
                                                <small className="fst-italic">{item.moTaChiTiet}</small>
                                            </div>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Menucpn;

//    return(
//        <div class="container-xxl py-5">
//        <div class="container">
//            <div class="text-center wow fadeInUp" data-wow-delay="0.1s">
//                <h5 class="section-title ff-secondary text-center text-primary fw-normal">Food Menu</h5>
//                <h1 class="mb-5">Món ăn phổ biến nhất</h1>
//            </div>
//            <div class="tab-class text-center wow fadeInUp" data-wow-delay="0.1s">
//                <ul class="nav nav-pills d-inline-flex justify-content-center border-bottom mb-5">
//                    <li class="nav-item">
//                        <a class="d-flex align-items-center text-start mx-3 ms-0 pb-3 active" data-bs-toggle="pill" href="#tab-1">
//                            <i class="fa fa-coffee fa-2x text-primary"></i>
//                            <div class="ps-3">
//                                <small class="text-body">Popular</small>
//                                <h6 class="mt-n1 mb-0">Bữa Sáng</h6>
//                            </div>
//                        </a>
//                    </li>
//                    <li class="nav-item">
//                        <a class="d-flex align-items-center text-start mx-3 pb-3" data-bs-toggle="pill" href="#tab-2">
//                            <i class="fa fa-hamburger fa-2x text-primary"></i>
//                            <div class="ps-3">
//                                <small class="text-body">Special</small>
//                                <h6 class="mt-n1 mb-0">Bữa Trưa</h6>
//                            </div>
//                        </a>
//                    </li>
//                    <li class="nav-item">
//                        <a class="d-flex align-items-center text-start mx-3 me-0 pb-3" data-bs-toggle="pill" href="#tab-3">
//                            <i class="fa fa-utensils fa-2x text-primary"></i>
//                            <div class="ps-3">
//                                <small class="text-body">Lovely</small>
//                                <h6 class="mt-n1 mb-0">Bữa Tối</h6>
//                            </div>
//                        </a>
//                    </li>
//                </ul>
//                <div class="tab-content">
//                    <div id="tab-1" class="tab-pane fade show p-0 active">
//                        <div class="row g-4">
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-1.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-2.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-3.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-4.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-5.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-6.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-7.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-8.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                        </div>
//                    </div>
//                    <div id="tab-2" class="tab-pane fade show p-0">
//                        <div class="row g-4">
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-1.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-2.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-3.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-4.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-5.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-6.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-7.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-8.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                        </div>
//                    </div>
//                    <div id="tab-3" class="tab-pane fade show p-0">
//                        <div class="row g-4">
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-1.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-2.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-3.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-4.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-5.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-6.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-7.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                            <div class="col-lg-6">
//                                <div class="d-flex align-items-center">
//                                    {/* <img class="flex-shrink-0 img-fluid rounded" src="img/menu-8.jpg" alt="" style="width: 80px;"> */}
//                                    <div class="w-100 d-flex flex-column text-start ps-4">
//                                        <h5 class="d-flex justify-content-between border-bottom pb-2">
//                                            <span>Chicken Burger</span>
//                                            <span class="text-primary">$115</span>
//                                        </h5>
//                                        <small class="fst-italic">Ipsum ipsum clita erat amet dolor justo diam</small>
//                                    </div>
//                                </div>
//                            </div>
//                        </div>
//                    </div>
//                </div>
//            </div>
//        </div>
//    </div>
//    );
//};export default Menucpn;