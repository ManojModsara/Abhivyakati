let generatedMemberID = '';

// Auto-generate member ID
function generateMemberID() {
    const prefix = 'ASS-JJN-';
    const randomNum = Math.floor(Math.random() * 9000) + 1000;
    return prefix + randomNum;
}

// Update preview card
function updatePreview() {
    const name = document.getElementById('fullName').value || 'आपका नाम';
    const designation = document.getElementById('designation').value || 'सदस्य';
    const blood = document.getElementById('bloodGroup').value || 'O+';
    const mobile = document.getElementById('mobile').value || '+91-XXXXXXXXXX';
    const village = document.getElementById('village').value || 'ग्राम';
    const panchayat = document.getElementById('panchayat').value || 'पंचायत';
    const district = document.getElementById('district').value || 'जिला';
    
    document.getElementById('preview-name').textContent = name;
    document.getElementById('preview-designation').textContent = designation;
    document.getElementById('preview-blood').textContent = blood;
    document.getElementById('preview-mobile').textContent = mobile.startsWith('+91') ? mobile : '+91-' + mobile;
    document.getElementById('preview-address').textContent = `${village}, ${panchayat}, ${district}`;
    
    // Generate new member ID when name changes
    if (name !== 'आपका नाम' && !generatedMemberID) {
        generatedMemberID = generateMemberID();
        document.getElementById('preview-member-id').textContent = generatedMemberID;
    }
}

// Word count for experience
function updateWordCount() {
    const experience = document.getElementById('experience').value;
    const words = experience.trim().split(/\s+/).filter(word => word.length > 0);
    const wordCount = words.length;
    document.getElementById('wordCount').textContent = wordCount;
    
    if (wordCount > 50) {
        document.getElementById('wordCount').style.color = 'red';
    } else {
        document.getElementById('wordCount').style.color = 'gray';
    }
}

// Save form data
function saveForm() {
    const formData = {
        fullName: document.getElementById('fullName').value,
        fatherName: document.getElementById('fatherName').value,
        dob: document.getElementById('dob').value,
        bloodGroup: document.getElementById('bloodGroup').value,
        designation: document.getElementById('designation').value,
        village: document.getElementById('village').value,
        panchayat: document.getElementById('panchayat').value,
        district: document.getElementById('district').value,
        pincode: document.getElementById('pincode').value,
        mobile: document.getElementById('mobile').value,
        altMobile: document.getElementById('altMobile').value,
        education: document.getElementById('education').value,
        experience: document.getElementById('experience').value,
        aadhaar: document.getElementById('aadhaar').value,
        pan: document.getElementById('pan').value,
        memberID: generatedMemberID
    };
    
    localStorage.setItem('membershipFormData', JSON.stringify(formData));
    alert('फॉर्म सेव हो गया है! / Form saved successfully!');
}

// Cancel form
function cancelForm() {
    if (confirm('क्या आप वाकई फॉर्म रद्द करना चाहते हैं? / Are you sure you want to cancel the form?')) {
        document.getElementById('membershipForm').reset();
        updatePreview();
        generatedMemberID = '';
    }
}

// Generate offer letter
function generateOfferLetter() {
    const name = document.getElementById('fullName').value;
    const designation = document.getElementById('designation').value;
    const memberID = generatedMemberID || generateMemberID();
    const today = new Date();
    
    // Update offer letter content
    document.getElementById('letterDate').textContent = today.toLocaleDateString('hi-IN');
    document.getElementById('letterName').textContent = name;
    document.getElementById('letterMemberName').textContent = name;
    document.getElementById('letterMemberCode').textContent = memberID;
    document.getElementById('letterDesignation').textContent = designation;
    document.getElementById('letterJoiningDate').textContent = today.toLocaleDateString('hi-IN');
    
    // Show offer letter section
    document.getElementById('offerLetterSection').classList.remove('hidden');
    document.getElementById('offerLetterSection').scrollIntoView({ behavior: 'smooth' });
}

// Print offer letter
function printOfferLetter() {
    const offerLetterCard = document.getElementById('offerLetterCard');
    
    // Create a new window for printing
    const printWindow = window.open('', '_blank');
    
    // Create the print document
    printWindow.document.write(`
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="UTF-8">
            <title>Offer Letter</title>
            <style>
                body { 
                    margin: 0; 
                    padding: 20px; 
                    background: white; 
                    font-family: Arial, sans-serif;
                }
                .offer-letter-card {
                    max-width: none !important;
                    margin: 0 !important;
                    box-shadow: none !important;
                    border: 2px solid #3b82f6 !important;
                }
                .no-print { display: none !important; }
                .hindi-text { font-family: Arial, sans-serif; }
                @page { margin: 0.5in; }
            </style>
        </head>
        <body>
            ${offerLetterCard.outerHTML}
        </body>
        </html>
    `);

    printWindow.document.close();

    // Wait for content to load then print
    setTimeout(() => {
        printWindow.print();
        printWindow.close();
    }, 500);
}

// Download offer letter as image
function downloadOfferLetter() {
    const offerLetterCard = document.getElementById('offerLetterCard');
    
    // Use html2canvas library to convert to image
    if (typeof html2canvas !== 'undefined') {
        // Show loading message
        const downloadBtn = event.target;
        const originalText = downloadBtn.innerHTML;
        downloadBtn.innerHTML = '<i class="fas fa-spinner fa-spin mr-2"></i>डाउनलोड हो रहा है...';
        downloadBtn.disabled = true;
        
        html2canvas(offerLetterCard, {
            scale: 3,
            useCORS: true,
            allowTaint: true,
            backgroundColor: '#ffffff'
        }).then(canvas => {
            // Create download link
            const link = document.createElement('a');
            const memberCode = document.getElementById('letterMemberCode').textContent;
            const memberName = document.getElementById('letterMemberName').textContent;
            link.download = `Offer-Letter-${memberCode}-${memberName.replace(/\s+/g, '-')}.png`;
            link.href = canvas.toDataURL('image/png', 1.0);
            
            // Trigger download
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
            
            // Restore button
            downloadBtn.innerHTML = originalText;
            downloadBtn.disabled = false;
            
            // Show success message
            alert('✅ ऑफर लेटर सफलतापूर्वक डाउनलोड हो गया! / Offer letter downloaded successfully!');
        }).catch(error => {
            console.error('Download error:', error);
            downloadBtn.innerHTML = originalText;
            downloadBtn.disabled = false;
            alert('❌ डाउनलोड में समस्या हुई। कृपया प्रिंट का उपयोग करें / Download failed. Please use print option');
        });
    } else {
        // Fallback to print if html2canvas is not available
        alert('डाउनलोड फीचर लोड हो रहा है... कृपया प्रिंट का उपयोग करें / Download feature loading... Please use print option');
        printOfferLetter();
    }
}

// Form submission
function handleFormSubmit(event) {
    event.preventDefault();
    
    // Validate required fields
    const requiredFields = ['fullName', 'fatherName', 'dob', 'village', 'panchayat', 'district', 'pincode', 'mobile', 'education', 'aadhaar'];
    let isValid = true;
    
    requiredFields.forEach(fieldId => {
        const field = document.getElementById(fieldId);
        if (!field.value.trim()) {
            field.style.borderColor = 'red';
            isValid = false;
        } else {
            field.style.borderColor = '';
        }
    });
    
    if (!isValid) {
        alert('कृपया सभी आवश्यक फील्ड भरें! / Please fill all required fields!');
        return;
    }
    
    // Generate member ID if not already generated
    if (!generatedMemberID) {
        generatedMemberID = generateMemberID();
        document.getElementById('preview-member-id').textContent = generatedMemberID;
    }
    
    // Save form data
    saveForm();
    
    // Generate and show offer letter
    generateOfferLetter();
    
    alert('आवेदन सफलतापूर्वक जमा हो गया! / Application submitted successfully!');
}

// Add event listeners
document.addEventListener('DOMContentLoaded', function() {
    // Update preview on input changes
    const inputs = ['fullName', 'designation', 'bloodGroup', 'mobile', 'village', 'panchayat', 'district'];
    inputs.forEach(id => {
        const element = document.getElementById(id);
        if (element) {
            element.addEventListener('input', updatePreview);
            element.addEventListener('change', updatePreview);
        }
    });

    // Word count for experience
    const experienceField = document.getElementById('experience');
    if (experienceField) {
        experienceField.addEventListener('input', updateWordCount);
    }

    // Form submission
    const form = document.getElementById('membershipForm');
    if (form) {
        form.addEventListener('submit', handleFormSubmit);
    }

    // Set current date for joining
    const today = new Date().toLocaleDateString('hi-IN');
    document.getElementById('preview-joining').textContent = today;

    // Load saved form data if exists
    const savedData = localStorage.getItem('membershipFormData');
    if (savedData) {
        const formData = JSON.parse(savedData);
        Object.keys(formData).forEach(key => {
            const element = document.getElementById(key);
            if (element && formData[key]) {
                element.value = formData[key];
            }
        });
        if (formData.memberID) {
            generatedMemberID = formData.memberID;
        }
        updatePreview();
    }

    // PAN number uppercase conversion
    const panField = document.getElementById('pan');
    if (panField) {
        panField.addEventListener('input', function() {
            this.value = this.value.toUpperCase();
        });
    }
});
