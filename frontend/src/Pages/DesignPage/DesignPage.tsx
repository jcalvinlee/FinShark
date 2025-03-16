import React from "react";
import Table from "../../Components/Table/Table";
import RatioList from "../../Components/RatioList/RatioList";
import {
  TestDataCompany,
  testIncomeStatementData,
} from "../../Components/Table/testData";
import { CompanyKeyMetrics } from "../../company";

type Props = {};
const data = TestDataCompany;

const tableConfig = [
  {
    label: "Market Cap",
    render: (company: CompanyKeyMetrics) => company.marketCap,
    subTitle: "Total value of all a company's shares of stock",
  },
];

const DesignPage = (props: Props) => {
  return (
    <>
      <h1>FinShark Design Page</h1>
      <h2>This is the design page.</h2>
      <RatioList data={testIncomeStatementData} config={tableConfig} />
      <Table />
    </>
  );
};

export default DesignPage;
