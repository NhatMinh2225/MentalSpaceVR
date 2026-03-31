# MentalSpaceVR

A small Unity prototype exploring how virtual environments can respond to 
emotional states. Users pick how they're feeling — Calm, Anxious, or Focused — 
using a PlayStation controller, and the forest around them shifts accordingly: 
lighting changes color, ambient music plays, and particles adjust to match the mood.

Built with Unity 6, C#, and Universal Render Pipeline.

## Demo
[Video Demo](https://youtu.be/7kc2q8fu-CA)

## Features
- PS Controller input for emotion selection
- Real-time lighting changes based on emotional state
- Ambient music per emotion (Calm / Anxious / Focused)
- Analog stick camera look (VR-style)
- Forest environment with fog and particle effects

## Tech Stack
- Unity 6 (URP)
- C#
- Unity Input System
- TextMeshPro

## Controls
| Button | Emotion |
|--------|---------|
| X | Calm |
| O | Anxious |
| Triangle | Focused |
| Right Stick | Look around |

## Context
Developed as a portfolio project for EVL internship application
at UIC School of Design — exploring VR applications in mental health.

## Changelog

### v1.1.0 (2026-03-30)
- Added breathing guide with 3 evidence-based patterns
- Added smooth locomotion with terrain collision
- Added skybox color change per emotion
- Added hint text UI for breathing guide

### v1.0.0 (2026-03-25)
- Initial release
- PS Controller emotion selection (Calm / Anxious / Focused)
- Real-time lighting and particle effects
- Ambient music per emotion
- VR-style camera look with analog stick
- Forest environment
