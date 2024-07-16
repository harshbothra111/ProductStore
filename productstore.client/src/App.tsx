import React, { useState } from 'react';
import CategorySelect from './components/CategorySelect';
import ProductList from './components/ProductList';

const App: React.FC = () => {
    const [categoryId, setCategoryId] = useState<number | null>(null);
    const [subCategoryId, setSubCategoryId] = useState<number | null>(null);

    return (
        <div>
            <p>Product Management</p>
            <div className='card'>
                <div className='card-header'>
                    <CategorySelect onCategoryChange={setCategoryId} onSubCategoryChange={setSubCategoryId} />
                </div>
                <div className='card-body'>
                    <ProductList categoryId={categoryId} subCategoryId={subCategoryId} />
                </div>
            </div>
        </div>
    );
};

export default App;