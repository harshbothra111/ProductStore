// src/components/CategorySelect.tsx
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Category } from '../interfaces/Category';
import { SubCategory } from '../interfaces/SubCategory';

interface CategorySelectProps {
    onCategoryChange: (categoryId: number | null) => void;
    onSubCategoryChange: (subCategoryId: number | null) => void;
}

const baseApiUrl = 'https://localhost:7048';

const CategorySelect: React.FC<CategorySelectProps> = ({ onCategoryChange, onSubCategoryChange }) => {
    const [categories, setCategories] = useState<Category[]>([]);
    const [subCategories, setSubCategories] = useState<SubCategory[]>([]);
    const [selectedCategoryId, setSelectedCategoryId] = useState<number | null>(null);

    useEffect(() => {
        fetchCategories();
    }, []);

    useEffect(() => {
        if (selectedCategoryId !== null) {
            fetchSubCategories(selectedCategoryId);
        } else {
            setSubCategories([]);
        }
    }, [selectedCategoryId]);

    const fetchCategories = async () => {
        try {
            const response = await axios.get<Category[]>(baseApiUrl + '/api/categories');
            setCategories(response.data);
        } catch (error) {
            console.error('Error fetching categories:', error);
        }
    };

    const fetchSubCategories = async (categoryId: number) => {
        try {
            const response = await axios.get<SubCategory[]>(`${baseApiUrl}/api/categories/${categoryId}/subcategories`);
            setSubCategories(response.data);
        } catch (error) {
            console.error('Error fetching subcategories:', error);
        }
    };

    const handleCategoryChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const categoryId = Number(event.target.value);
        setSelectedCategoryId(categoryId);
        onCategoryChange(categoryId);
        onSubCategoryChange(null); // Reset the subcategory selection
    };

    const handleSubCategoryChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const subCategoryId = Number(event.target.value);
        onSubCategoryChange(subCategoryId);
    };

    return (
        <div className="row g-3 align-items-center">
            <div className="col-auto">
                <label className="col-form-label">Category</label>
            </div>
            <div className="col-auto">
                <select className="form-select" value={selectedCategoryId || ''} onChange={handleCategoryChange}>
                    <option value="">Select a category</option>
                    {categories.map((category) => (
                        <option key={category.id} value={category.id}>
                            {category.name}
                        </option>
                    ))}
                </select>
            </div>
            <div className="col-auto">
                <label className="col-form-label">Sub Category</label>
            </div>
            <div className="col-auto">
                <select className="form-select"
                    onChange={handleSubCategoryChange}
                    disabled={selectedCategoryId === null}
                >
                    <option value="">Select a subcategory</option>
                    {subCategories.map((subCategory) => (
                        <option key={subCategory.id} value={subCategory.id}>
                            {subCategory.name}
                        </option>
                    ))}
                </select>

            </div>
        </div>
    );
};

export default CategorySelect;