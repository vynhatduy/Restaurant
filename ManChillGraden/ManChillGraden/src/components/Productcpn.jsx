import React from 'react';

const ProductList = () => {
    // Danh sách sản phẩm mẫu
    const products = [

        {
            idSanPham: 1,
            tenSanPham: 'Cơm Tấm',
            hinhAnh: 'https://via.placeholder.com/150',
            moTa: "Cơm Tấm ",  
            donGia: "100.000đ",
            soSao:"5.0",
            trangThai:true,
            loai:"Cơm"
        },
        {
            idSanPham: 2,
            tenSanPham: 'Cơm Tấm',
            hinhAnh: 'https://via.placeholder.com/150',
            moTa: "Cơm Tấm ",  
            donGia: "100.000đ",
            soSao:"5.0",
            trangThai:true,
            loai:"Cơm"
        },
        {
            idSanPham: 3,
            tenSanPham: 'Cơm Tấm',
            hinhAnh: 'https://via.placeholder.com/150',
            moTa: "Cơm Tấm ",  
            donGia: "100.000đ",
            soSao:"5.0",
            trangThai:true,
            loai:"Cơm"
        },
        {
            idSanPham: 4,
            tenSanPham: 'Cơm Tấm',
            hinhAnh: 'https://via.placeholder.com/150',
            moTa: "Cơm Tấm ",  
            donGia: "100.000đ",
            soSao:"5.0",
            trangThai:true,
            loai:"Cơm"
        },
        {
            id: 5,
            name: 'Cơm Tấm',
            address: 'Đà lạt',
            time: '9:00 AM - 9:00 PM',
            imageUrl: 'https://via.placeholder.com/150',
            price: "100.000đ",
        },
        {
            id: 6,
            name: 'Cơm Tấm',
            address: 'Đà lạt',
            time: '9:00 AM - 9:00 PM',
            imageUrl: 'https://via.placeholder.com/150',
            price: "100.000đ",
        },
        {
            id: 7,
            name: 'Cơm Tấm',
            address: 'Đà lạt',
            time: '9:00 AM - 9:00 PM',
            imageUrl: 'https://via.placeholder.com/150',
            price: "100.000đ",
        },
        {
            id: 8,
            name: 'Cơm Tấm',
            address: 'Đà lạt',
            time: '9:00 AM - 9:00 PM',
            imageUrl: 'https://via.placeholder.com/150',
            price: "100.000đ",
        },
    ];

    return (
        <div className="container mt-1">
            <div className="row">
                {products.map((product) => (
                    <div className="col-md-3 mb-4"  key={product.idSanPham}>
                        <div className="card text-start shadow p-1 mb-5 bg-white rounded">
                            <img src={product.hinhAnh} className="card-img-top" alt={product.tenSanPham} />
                            <div className="card-body">
                                <h5 className="card-title">{product.tenSanPham}</h5>
                            </div>
                            <ul className="text-start">
                                <li className="list-group-item" style={{ listStyle: 'none' }}>
                                    {product.moTa}
                                </li>
                                <li className="list-group-item fw-bold" style={{ listStyle: 'none' }}>
                                    {product.loai}
                                </li>
                                <li className="list-group-item fw-bold text-danger" style={{ listStyle: 'none' }}>
                                    {product.donGia}
                                </li>
                            </ul>
                            <div className="d-flex justify-content-between">
                                <button type="button" className="btn btn-primary mx-4">Chọn</button>
                                <button type="button" className="btn btn-primary mx-4">Chi tiết</button>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default ProductList;
