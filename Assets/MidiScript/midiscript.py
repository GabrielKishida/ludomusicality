import mido
import os
import numpy as np

snare_note = 38
kick_note = 35

def extract_note_hit_times(midi_file_path, note):
    snare_hit_times = np.array([])

    # Open the MIDI file
    midi_file = mido.MidiFile(midi_file_path)

    # Define the channel for the drums (usually channel 9)
    drums_channel = 9

    # Get tempo and ticks per beat from the MIDI file
    ticks_per_beat = midi_file.ticks_per_beat
    tempo = 500000  # Default tempo if not found in MIDI file

    # Iterate through each track in the MIDI file
    for track in midi_file.tracks:
        absolute_time = 0
        for msg in track:
            absolute_time += msg.time
            if msg.type == 'set_tempo':
                tempo = msg.tempo

            # Check if the message is a note-on event and on the drums channel
            elif msg.type == 'note_on' and msg.channel == drums_channel:
                # If the note is a snare hit (you can adjust the note number if needed)
                if msg.note == note:
                    # Convert tick time to milliseconds
                    time_in_ms = mido.tick2second(absolute_time, ticks_per_beat, tempo)
                    snare_hit_times = np.append(snare_hit_times, time_in_ms)

    return np.unique(snare_hit_times)

# Example usage
script_dir = os.path.dirname(os.path.abspath(__file__))  # Get directory of the current script
midi_file_path = os.path.join(script_dir, "level1base.mid") 
snare_hit_times = extract_note_hit_times(midi_file_path, snare_note)
kick_hit_times = extract_note_hit_times(midi_file_path, kick_note)

output_snare_path = file_path = os.path.join(script_dir,"snare_times.txt")
output_kick_path = file_path = os.path.join(script_dir,"kick_times.txt")

# Write array to text file
np.savetxt(output_snare_path, snare_hit_times, fmt='%.3f')
np.savetxt(output_kick_path, kick_hit_times, fmt='%.3f')