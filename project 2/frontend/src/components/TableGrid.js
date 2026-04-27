function TableGrid({ rows, onCellChange }) {
  const columns = [
    { key: 'name', label: 'Student Name', editable: false },
    ...Array.from({ length: 12 }, (_, index) => ({ key: `lab${index + 1}`, label: `Lab ${index + 1}`, editable: true })),
    { key: 'cie1', label: 'CIE1', editable: true },
    { key: 'cie2', label: 'CIE2', editable: true },
    { key: 'cie3', label: 'CIE3', editable: true },
    { key: 'total', label: 'Total', editable: false },
    { key: 'finalOutOf50', label: 'Final', editable: false }
  ];

  return (
    <div className="table-wrapper">
      <table>
        <thead>
          <tr>
            {columns.map((column) => (
              <th key={column.key}>{column.label}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {rows.map((row) => (
            <tr key={row.studentId}>
              {columns.map((column) => (
                <td key={column.key}>
                  {column.editable ? (
                    <input
                      type="number"
                      min="0"
                      max={column.key.startsWith('lab') ? 15 : 10}
                      value={row[column.key] ?? 0}
                      onChange={(event) => onCellChange(row.studentId, column.key, Number(event.target.value))}
                    />
                  ) : (
                    <span>{row[column.key]}</span>
                  )}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default TableGrid;
