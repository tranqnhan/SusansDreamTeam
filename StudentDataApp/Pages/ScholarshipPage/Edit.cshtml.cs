﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentDataApp.Data;
using StudentDataApp.Models;

namespace StudentDataApp.Pages.ScholarshipPage
{
    public class EditModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public EditModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Scholarship Scholarship { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Scholarship = await _context.Scholarship.FirstOrDefaultAsync(m => m.StudentId == id);

            if (Scholarship == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Scholarship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScholarshipExists(Scholarship.StudentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ScholarshipExists(int id)
        {
            return _context.Scholarship.Any(e => e.StudentId == id);
        }
    }
}