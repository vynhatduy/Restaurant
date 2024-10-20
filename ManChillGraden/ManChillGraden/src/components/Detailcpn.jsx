import React from 'react'
const DetailList = () => {
    const Details = [
        {
            id: 1,
            title: 'Phở Bò',
            description: 'Món ăn truyền thống với nước dùng thơm ngon và thịt bò tươi.',
            updated: '3 phút trước',
            imageUrl: 'https://via.placeholder.com/150',
          },

    ];



    return (
        <div className="container mt-12">
            <div className="row">
                {Details.map((product) => (
                    <div className="" key={product.id}>
                        <div className="card ">
                            <img src={product.imageUrl} className="card-img-top" alt={product.title} />
                            <div className="card-body">
                                <h5 className="card-title">{product.title}</h5>
                                <p className="card-text">{product.description}</p>
                                <p className="card-text">
                                    <small className="text-body-secondary">{product.updated}</small>
                                </p>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>);
}; export default DetailList;