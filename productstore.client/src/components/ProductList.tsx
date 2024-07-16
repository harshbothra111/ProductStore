import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Product } from '../interfaces/Product'
import { PaginationResponse } from '../interfaces/PaginationResponse';

interface ProductListProps {
    categoryId: number | null;
    subCategoryId: number | null;
}

const baseApiUrl = 'https://localhost:7048/';

const ProductList: React.FC<ProductListProps> = ({ categoryId, subCategoryId }) => {
    const [products, setProducts] = useState<Product[]>([]);
    const [pageNumber, setPage] = useState(0);
    const [totalPages, setTotalPages] = useState(1);

    useEffect(() => {
        fetchProducts();
    }, [pageNumber, categoryId, subCategoryId]);

    const fetchProducts = async () => {
        const params: { [key: string]: any } = { pageNumber: pageNumber };
        if (categoryId !== null) params.categoryId = categoryId;
        if (subCategoryId !== null) params.subCategoryId = subCategoryId;

        try {
            const response = await axios.get<PaginationResponse<Product>>(baseApiUrl + 'api/products', { params });
            setProducts(response.data.data);
            setTotalPages(response.data.totalPages);
        } catch (error) {
            console.error('Error fetching products:', error);
        }
    };

    return (
        <div>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Quantity</th>
                        <th>Code</th>
                        <th>Price</th>
                        <th>Description</th>
                        <th>Image</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map((product) => (
                        <tr key={product.id}>
                            <td>{product.name}</td>
                            <td>{product.quantity}</td>
                            <td>{product.code}</td>
                            <td>{product.price}</td>
                            <td>{product.description}</td>
                            <td><img src={baseApiUrl + product.imageUrl} alt={product.name} width="50" /></td>
                        </tr>
                    ))}
                </tbody>
            </table>
            <div>
                <nav aria-label="Page navigation example">
                    <ul className="pagination justify-content-center">
                        <li className={`page-item ${(pageNumber === 0) ? 'disabled' : ''}`} onClick={() => setPage(pageNumber - 1)}>
                            <a className="page-link" href="#">Previous</a>
                        </li>
                        <li className="page-item"><a className="page-link" href="#">Page {pageNumber + 1} of {totalPages}</a></li>
                        <li className={`page-item ${(pageNumber + 1 === totalPages) ? 'disabled' : ''}`} onClick={() => setPage(pageNumber + 1)}>
                            <a className="page-link" href="#">Next</a>
                        </li>
                    </ul>
                </nav>
            </div>
        </div>
    );
};

export default ProductList;
